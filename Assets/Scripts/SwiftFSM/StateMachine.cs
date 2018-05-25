using System;

public class StateMachine<TState, TEvent, TContext> 
	where TState : IComparable 
	where TEvent : IComparable
{
	public TState CurrentStateId {
		get {
			return CurrentState.StateId;
		}
	}

	private IInnerState<TState, TEvent, TContext> _currentState;
	private IInnerState<TState, TEvent, TContext> CurrentState { 
		get {
			return _currentState;
		}
		set
		{
			var oldState = _currentState;
			if (oldState != null)
			{
				oldState.Exit();
			}

			_currentState = value;

			if (value != null)
			{
				value.Enter();
			}
			
		}
	}
	private IInnerState<TState, TEvent, TContext> InitialState { get;set; }

	private StateDictionary<TState, TEvent, TContext> stateDict;
	private IFactory<TState, TEvent, TContext> factory;

	public StateMachine()
	{
		factory = new Factory<TState, TEvent, TContext>();
		stateDict = new StateDictionary<TState, TEvent, TContext>(factory);
	}

	private bool isRuning = false;
	private bool isInitialized = false;
	private TContext context;
	public void Initialize(TState stateId, TContext ctx)
	{
		isInitialized = true;
		InitialState = stateDict[stateId];
		context = ctx;
	}

	public void Start()
	{
		isRuning = true;

		if (CurrentState == null)
		{
			CurrentState = InitialState;
		}
		
	}

	public void Stop()
	{
		isRuning = false;

		if (CurrentState != null)
		{
			CurrentState.Exit();
		}
	}

	public bool IsStarted
	{
		get
		{
			return isRuning;
		}
	}

	public virtual void Execute()
	{
		Check_StateMachineHasInitializedAndIsRunning();

		if (CurrentState == null)
			CurrentState = InitialState;

		CurrentState.Execute();
	}

	public void Fire(TEvent evtId, params object[] parameters)
	{
		Check_StateMachineHasInitializedAndIsRunning();
		
		var result = CurrentState.Fire(evtId, parameters);

		if (result.IsFired)
		{
			CurrentState = result.ToState;
		}

	}

	private void Check_StateMachineHasInitializedAndIsRunning()
	{
		if (!isInitialized)
		{
			throw new InvalidOperationException("Cannot execute before state machine is initialized");
		}

		if (!isRuning)
		{
			throw new InvalidOperationException("Cannot execute before state machine is running");
		}
	}


	public IInSyntax<TState, TEvent, TContext> In(TState state)
	{
		var s = stateDict[state];
		var builder = new StateBuilder<TState, TEvent, TContext>(s, stateDict, factory);

		return builder;
	}

}
