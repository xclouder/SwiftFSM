using System;
using System.Collections;

internal interface ITransition <TState, TEvent, TContext>
	where TState : IComparable
	where TEvent : IComparable
{

	IInnerState<TState, TEvent, TContext> Source {get;set;}
	IInnerState<TState, TEvent, TContext> Target {get;set;}
	TEvent EventToTrigger {get;set;}

}
