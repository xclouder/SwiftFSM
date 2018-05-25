# SwiftFSM
一个轻量级的有限状态机实现

示例

```
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

public void TestBasicMachineBehaviour()
{
	var machine = new StateMachine<MyState, MyEvent>();

//指定初始状态
	machine.Initialize(MyState.StateA);

//定义状态跳转
	machine	.In(MyState.StateA)
				.ExecuteOnEnter(()=>{UnityEngine.Debug.Log("Enter State A");})
				.ExecuteOnExit(()=>{UnityEngine.Debug.Log("Exit State A");})
			.On(MyEvent.EventA)
			.GoTo(MyState.StateB);

	machine.In(MyState.StateB).On(MyEvent.EventB).GoTo(MyState.StateA);

//启动状态机
	machine.Start();
	machine.Execute();

//触发事件，状态跳转
	machine.Fire(MyEvent.EventA);
	var stateId = machine.CurrentStateId;
	Assert.AreEqual(MyState.StateB, stateId);

	machine.Fire(MyEvent.EventB);
	stateId = machine.CurrentStateId;
	Assert.AreEqual(MyState.StateA, stateId);
}
```
