using System;
using System.Collections;
using System.Collections.Generic;

internal class StateDictionary<TState, TEvent, TContext> 
	where TState : IComparable
	where TEvent : IComparable
{
	private IDictionary<TState, IInnerState<TState, TEvent, TContext>> dict;
	private IFactory<TState, TEvent, TContext> factory;

	public StateDictionary(IFactory<TState, TEvent, TContext> fac)
	{
		dict = new Dictionary<TState, IInnerState<TState, TEvent, TContext>>();
		factory = fac;
	}

	public IInnerState<TState, TEvent, TContext> this[TState stateId]
	{
		get {

			if (dict.ContainsKey(stateId))
			{
				return dict[stateId];
			}

			var s = factory.Create(stateId);
			dict.Add(stateId, s);

			return s;
		}
	}
}
