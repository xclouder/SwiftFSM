using System;
using System.Collections;

internal interface IInnerState <TState, TEvent, TContext> 
	where TState : IComparable 
	where TEvent : IComparable
{

	TState StateId {get;}

	TransitionResult<TState, TEvent, TContext> Fire(TEvent eventId, params object[] parameters);
	void AddTransition(ITransition<TState, TEvent, TContext> tr);

	void AttachStateObject(IState state);

	Action ExecuteOnEnterAction {set;}
	Action ExecuteOnExitAction {set;}
	Action ExecuteAction {set;}

	void Enter();
	void Execute();
	void Exit();
}
