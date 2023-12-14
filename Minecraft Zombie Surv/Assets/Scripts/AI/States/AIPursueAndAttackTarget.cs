using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPursueAndAttackTarget : AIStateBase
{
    [SerializeField] private NavMeshAgent agent;

    [Space]
    [SerializeField] private float pursueSpeed;
    [SerializeField] private float pursueStoppingDistance;

    [Space]
    [SerializeField] private LayerMask targetsLayer;
    [SerializeField] private float targetsCheckRadius;

    [Space]
    [ReadOnly, SerializeField] private Collider target;

    public override bool OnValidateState()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, targetsCheckRadius, targetsLayer);

        if (colliders.Length > 0)
        {
            target = colliders[0];
            return true;
        }

        return false;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        agent.speed = pursueSpeed;
        agent.stoppingDistance = pursueStoppingDistance;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();

        if (!target) return;

        agent.SetDestination(target.transform.position);
    }
}
