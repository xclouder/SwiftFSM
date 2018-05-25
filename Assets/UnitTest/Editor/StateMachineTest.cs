using System;

using NUnit.Framework;

public class StateMachineTest {

	class CustomState : IState
	{
		public virtual void Enter()
		{
			UnityEngine.Debug.Log("CustomState Enter");
		}
		
		public virtual void Execute()
		{
			UnityEngine.Debug.Log("CustomState Execute");
		}
		
		public virtual void Exit()
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

		machine.Initialize(MyState.StateA);
		ex = Assert.Throws<InvalidOperationException>(() => machine.Execute());
		Assert.That(ex.Message, Is.EqualTo("Cannot execute before state machine is running"));

		machine	.In(MyState.StateA).Attach(new CustomState())
					.ExecuteOnEnter(()=>{UnityEngine.Debug.Log("Enter State A");})
					.ExecuteOnExit(()=>{UnityEngine.Debug.Log("Exit State A");})
				.On(MyEvent.EventA)
				.GoTo(MyState.StateB);

		machine.In(MyState.StateB).On(MyEvent.EventB).GoTo(MyState.StateA);

		machine.Start();
		machine.Execute();
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
