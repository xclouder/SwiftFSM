using System;
using System.Collections;

internal class TransitionResult<TState, TEvent, TContext> 
	where TState : IComparable
	where TEvent : IComparable
	where TContext : class
{

	public TransitionResult (bool fired, IInnerState<TState, TEvent, TContext> toState, object[] parameters)
	{
		IsFired = fired;
		ToState = toState;
		Parameters = parameters;
	}

	public bool IsFired {get;set;}

	public IInnerState<TState, TEvent, TContext> ToState {get;set;}

	public object[] Parameters;

}
