using System;
using System.Collections;

internal class StateBuilder<TState, TEvent, TContext> : IInSyntax<TState, TEvent, TContext>, IOnSyntax<TState, TEvent, TContext>
	where TState : IComparable
	where TEvent : IComparable
{
	private StateDictionary<TState, TEvent, TContext> StateDict {get;set;}
	private IFactory<TState, TEvent, TContext> Factory {get;set;}

	private IInnerState<TState, TEvent, TContext> CurrentState {get;set;}
	private TEvent CurrentEvent {get;set;}

	public StateBuilder(IInnerState<TState, TEvent, TContext> state, StateDictionary<TState, TEvent, TContext> stateDict, IFactory<TState, TEvent, TContext> factory)
	{
		StateDict = stateDict;
		Factory = factory;

		CurrentState = state;
	}

	IInSyntax<TState, TEvent, TContext> IInSyntax<TState, TEvent, TContext>.ExecuteOnEnter(Action enterAction)
	{
		CurrentState.ExecuteOnEnterAction = enterAction;
		return this;
	}

	IInSyntax<TState, TEvent, TContext> IInSyntax<TState, TEvent, TContext>.Execute(Action executeAction)
	{
		CurrentState.ExecuteAction = executeAction;

		return this;
	}

	IInSyntax<TState, TEvent, TContext> IInSyntax<TState, TEvent, TContext>.ExecuteOnExit(Action exitAction)
	{
		CurrentState.ExecuteOnExitAction = exitAction;

		return this;
	}

	IOnSyntax<TState, TEvent, TContext> IInSyntax<TState, TEvent, TContext>.On(TEvent evt)
	{
		CurrentEvent = evt;

		return this;
	}

	IInSyntax<TState, TEvent, TContext> IOnSyntax<TState, TEvent, TContext>.GoTo(TState state)
	{
		var toState = StateDict[state];
		var tr = Factory.CreateTransition(CurrentEvent, toState);
		CurrentState.AddTransition(tr);

		return this;
	}

	IInSyntax<TState, TEvent, TContext> IInSyntax<TState, TEvent, TContext>.Attach(IState attachedState)
	{
		CurrentState.AttachStateObject(attachedState);

		return this;
	}
}
