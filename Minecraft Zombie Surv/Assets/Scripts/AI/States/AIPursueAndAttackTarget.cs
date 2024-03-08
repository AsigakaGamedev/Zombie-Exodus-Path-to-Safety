using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPursueAndAttackTarget : AIStateBase
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private FieldOfView vision;
    [SerializeField] private AnimationsController animations;

    [Space]
    [SerializeField] private AttacksHandler attacks;
    [SerializeField] private float attackDistance;

    [Space]
    [SerializeField] private float pursueSpeed;
    [SerializeField] private float pursueStoppingDistance;

    [Space]
    [ReadOnly, SerializeField] private Transform target;

    private Vector3 prevTargetPos;

    public override void OnInit()
    {
        base.OnInit();

        attacks.Init();
    }

    public override bool OnValidateState()
    {
        target = vision.DetectColliderInVision();

        return target;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        animations.SetMove(true);
        animations.SetBool("isPursue", true);

        agent.isStopped = false;
        agent.speed = pursueSpeed;
        agent.stoppingDistance = pursueStoppingDistance;

        prevTargetPos = target.position;
        agent.SetDestination(target.position);
    }

    public override void OnExitState()
    {
        base.OnExitState();

        animations.SetMove(false);
        animations.SetBool("isPursue", false);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();

        if (!target) return;

        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            Vector3 dirToTarget = target.position - transform.position;
            dirToTarget.Normalize();
            
            transform.rotation = Quaternion.LookRotation(dirToTarget);

            if (attacks.TryAttack())
            {
                animations.SetAttackTrigger();
            }
        }

        if (prevTargetPos != target.position)
        {
            agent.SetDestination(target.position);
            prevTargetPos = target.position;
        }
    }
}
