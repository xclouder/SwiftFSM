using System;
using System.Collections;

public interface IInSyntax<TState, TEvent, TContext>
	where TState : IComparable
	where TEvent : IComparable
{
	IInSyntax<TState, TEvent, TContext> ExecuteOnEnter(Action<TContext, object[]> enterAction);
	IInSyntax<TState, TEvent, TContext> Execute(Action<TContext> executeAction);
	IInSyntax<TState, TEvent, TContext> ExecuteOnExit(Action<TContext, object[]> enterAction);
	IOnSyntax<TState, TEvent, TContext> On(TEvent evt);
	IInSyntax<TState, TEvent, TContext> Attach(IState<TContext> attachedState);
}
