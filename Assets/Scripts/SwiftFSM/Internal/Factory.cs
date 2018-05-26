using System;
using System.Collections;

internal class Factory<TState, TEvent, TContext> : IFactory<TState, TEvent, TContext> 
	where TState : IComparable
	where TEvent : IComparable
	where TContext : class
{

	public IInnerState<TState, TEvent, TContext> Create(TState stateId)
	{
		return new InnerState<TState, TEvent, TContext>(stateId, null);
	}

	public ITransition<TState, TEvent, TContext> CreateTransition(TEvent evtId, IInnerState<TState, TEvent, TContext> target)
	{
		var tr = new Transition<TState, TEvent, TContext>();
		tr.Target = target;
		tr.EventToTrigger = evtId;

		return tr;
	}

}
