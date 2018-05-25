using System;
using System.Collections;

public interface IInSyntax<TState, TEvent, TContext>
	where TState : IComparable
	where TEvent : IComparable
{
	IInSyntax<TState, TEvent, TContext> ExecuteOnEnter(Action enterAction);
	IInSyntax<TState, TEvent, TContext> Execute(Action executeAction);
	IInSyntax<TState, TEvent, TContext> ExecuteOnExit(Action enterAction);
	IOnSyntax<TState, TEvent, TContext> On(TEvent evt);
	IInSyntax<TState, TEvent, TContext> Attach(IState attachedState);
}
