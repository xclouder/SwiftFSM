using System;
using System.Collections;
using System.Collections.Generic;

internal class InnerState<TState, TEvent, TContext> : IInnerState<TState, TEvent, TContext> 
	where TState : IComparable
	where TEvent : IComparable 
{

    private IList<ITransition<TState, TEvent, TContext>> transitions;

	public TState StateId {
		get;
		private set;
	}

	internal InnerState (TState stateId, IState attachedStateEntity)
	{
		StateId = stateId;
		AttachedState = attachedStateEntity;
	}

	public void Enter()
	{
		if (ExecuteOnEnterAction != null)
			ExecuteOnEnterAction();

		if (AttachedState != null)
			AttachedState.Enter();
	}

	public void Execute()
	{
		if (ExecuteAction != null)
			ExecuteAction();

		if (AttachedState != null)
			AttachedState.Execute();
	}

	public void Exit()
	{
		if (ExecuteOnExitAction != null)
			ExecuteOnExitAction();

		if (AttachedState != null)
			AttachedState.Exit();
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

		return new TransitionResult<TState, TEvent, TContext>(fired, toState);
	}

	public void AddTransition(ITransition<TState, TEvent, TContext> tr)
	{
		if (transitions == null)
			transitions = new List<ITransition<TState, TEvent, TContext>>();

		transitions.Add(tr);
	}


	public IState AttachedState {get; private set;}
	public void AttachStateObject(IState state)
	{
		AttachedState = state;
	}

	public Action ExecuteOnEnterAction {set; private get;}
	public Action ExecuteOnExitAction {set; private get;}
	public Action ExecuteAction {set; private get;}

}
