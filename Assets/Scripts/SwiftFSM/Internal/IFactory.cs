using System;
using System.Collections;

internal interface IFactory<TState, TEvent, TContext>
	where TState : IComparable
	where TEvent : IComparable
	where TContext : class
{
	IInnerState<TState, TEvent, TContext> Create(TState stateId);
	ITransition<TState, TEvent, TContext> CreateTransition(TEvent evtId, IInnerState<TState, TEvent, TContext> target);
}
