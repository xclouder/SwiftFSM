using System;
using System.Collections;
using System.Collections.Generic;

internal class InnerState<TState, TEvent, TContext> : IInnerState<TState, TEvent, TContext> 
	where TState : IComparable
	where TEvent : IComparable 
	where TContext : class
{

    private IList<ITransition<TState, TEvent, TContext>> transitions;

	public TState StateId {
		get;
		private set;
	}

	internal InnerState (TState stateId, IState<TContext> attachedStateEntity)
	{
		StateId = stateId;
		AttachedState = attachedStateEntity;
	}

	public void Enter(TContext ctx, params object[] parameters)
	{
		if (ExecuteOnEnterAction != null)
			ExecuteOnEnterAction(ctx, parameters);

		if (AttachedState != null)
			AttachedState.Enter(ctx, parameters);
	}

	public void Execute(TContext ctx)
	{
		if (ExecuteAction != null)
			ExecuteAction(ctx);

		if (AttachedState != null)
			AttachedState.Execute(ctx);
	}

	public void Exit(TContext ctx, params object[] parameters)
	{
		if (ExecuteOnExitAction != null)
			ExecuteOnExitAction(ctx, parameters);

		if (AttachedState != null)
			AttachedState.Exit(ctx, parameters);
	}
	
	public TransitionResult<TState, TEvent, TContext> Fire(TEvent eventId, params object[] paramters)
	{
        bool fired = false;
		IInnerState<TState, TEvent, TContext> toState = null;
		if (transitions != null)
		{
			foreach (var t in transitions)
			{
				if (t.EventToTrigger.Equals(eventId))
				{
					fired = true;
					toState = t.Target;
				}
			}
		}

		return new TransitionResult<TState, TEvent, TContext>(fired, toState, paramters);
	}

	public void AddTransition(ITransition<TState, TEvent, TContext> tr)
	{
		if (transitions == null)
			transitions = new List<ITransition<TState, TEvent, TContext>>();

		transitions.Add(tr);
	}


	private IState<TContext> AttachedState {get; set;}
	public void AttachStateObject(IState<TContext> state)
	{
		AttachedState = state;
	}

	public Action<TContext, object[]> ExecuteOnEnterAction {set; private get;}
	public Action<TContext, object[]> ExecuteOnExitAction {set; private get;}
	public Action<TContext> ExecuteAction {set; private get;}
	
}
