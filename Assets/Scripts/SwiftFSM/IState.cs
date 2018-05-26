using UnityEngine;
using System.Collections;

public interface IState<in TContext> {

	void Enter(TContext ctx, params object[] args);

	void Execute(TContext ctx);

	void Exit(TContext ctx, params object[] args);

}
