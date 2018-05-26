using System;
using System.Collections;

internal class Transition<TState, TEvent, TContext> : ITransition <TState, TEvent, TContext>
	where TState : IComparable
	where TEvent : IComparable
	where TContext : class
{
	public IInnerState<TState, TEvent, TContext> Source {get;set;}
	public IInnerState<TState, TEvent, TContext> Target {get;set;}
	public TEvent EventToTrigger {get;set;}

//	private IStateMachine machine;

}
