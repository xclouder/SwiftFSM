using System;
using System.Collections;

public interface IOnSyntax <TState, TEvent, TContext>
	where TState : IComparable
	where TEvent : IComparable
{
	IInSyntax<TState, TEvent, TContext> GoTo(TState state);
}
