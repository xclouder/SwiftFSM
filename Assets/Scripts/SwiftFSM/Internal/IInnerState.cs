using System;
using System.Collections;

internal interface IInnerState <TState, TEvent, TContext> 
	where TState : IComparable 
	where TEvent : IComparable
	where TContext : class
{

	TState StateId {get;}

	TransitionResult<TState, TEvent, TContext> Fire(TEvent eventId, params object[] parameters);
	void AddTransition(ITransition<TState, TEvent, TContext> tr);

	void AttachStateObject(IState<TContext> state);

	Action<TContext, object[]> ExecuteOnEnterAction {set;}
	Action<TContext, object[]> ExecuteOnExitAction {set;}
	Action<TContext> ExecuteAction {set;}
	
	void Enter(TContext ctx, params object[] parameters);
	void Execute(TContext ctx);
	void Exit(TContext ctx, params object[] parameters);
	
}


