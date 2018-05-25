﻿using System;
using System.Collections;

internal class TransitionResult<TState, TEvent, TContext> 
	where TState : IComparable
	where TEvent : IComparable
{

	public TransitionResult (bool fired, IInnerState<TState, TEvent, TContext> toState)
	{
		IsFired = fired;
		ToState = toState;
	}

	public bool IsFired {get;set;}

	public IInnerState<TState, TEvent, TContext> ToState {get;set;}

}
