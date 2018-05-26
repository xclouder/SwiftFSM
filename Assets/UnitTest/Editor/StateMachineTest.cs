using System;

using NUnit.Framework;
using UnityEngine;

public class StateMachineTest {

	class CustomState : IState<object>
	{
		public virtual void Enter(object ctx, params object[] args)
		{
			var g = ctx as GameObject;
			Assert.AreEqual(g.name, "a");
			UnityEngine.Debug.Log("CustomState Enter");
		}
		
		public virtual void Execute(object ctx)
		{
			UnityEngine.Debug.Log("CustomState Execute");
		}
		
		public virtual void Exit(object ctx, params object[] args)
		{
			UnityEngine.Debug.Log("CustomState Exit");
		}

	}

	enum MyState
	{
		StateA,
		StateB
	}

	enum MyEvent
	{
		EventA,
		EventB
	}

	[Test]
	public void TestBasicMachineBehaviour()
	{
		var machine = new StateMachine<MyState, MyEvent, System.Object>();

		var ex = Assert.Throws<InvalidOperationException>(() => machine.Execute());
		Assert.That(ex.Message, Is.EqualTo("Cannot execute before state machine is initialized"));

		machine.Initialize(MyState.StateA, new GameObject("a"));
		ex = Assert.Throws<InvalidOperationException>(() => machine.Execute());
		Assert.That(ex.Message, Is.EqualTo("Cannot execute before state machine is running"));

		machine	.In(MyState.StateA).Attach(new CustomState())
					.ExecuteOnEnter((ctx, args)=>{UnityEngine.Debug.Log("Enter State A");})
					.ExecuteOnExit((ctx, args)=>{UnityEngine.Debug.Log("Exit State A");})
				.On(MyEvent.EventA)
				.GoTo(MyState.StateB);

		machine.In(MyState.StateB).On(MyEvent.EventB).GoTo(MyState.StateA);

		machine.Start();
		var stateId = machine.CurrentStateId;
		Assert.AreEqual(stateId, MyState.StateA);

		machine.Fire(MyEvent.EventA);
		stateId = machine.CurrentStateId;
		Assert.AreEqual(MyState.StateB, stateId);

		machine.Fire(MyEvent.EventB);
		stateId = machine.CurrentStateId;
		Assert.AreEqual(MyState.StateA, stateId);
	}

}
