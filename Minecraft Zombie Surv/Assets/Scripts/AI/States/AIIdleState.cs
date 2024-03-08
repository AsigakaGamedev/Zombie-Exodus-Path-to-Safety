using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIIdleState : AIStateBase
{
    [SerializeField] private AnimationsController animations;
    [SerializeField] private NavMeshAgent agent;

    public override void OnEnterState()
    {
        base.OnEnterState();

        agent.isStopped = true;
        animations.SetMove(false);
    }

    public override void OnExitState()
    {
        base.OnExitState();

        agent.isStopped = false;
    }
}
