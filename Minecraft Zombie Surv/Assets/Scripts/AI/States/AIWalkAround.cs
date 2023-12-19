using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWalkAround : AIStateBase
{
    [SerializeField] private NavMeshAgent agent;

    [Space]
    [SerializeField] private LayerMask walkableLayers;
    [SerializeField] private float walkableRadius;
    [SerializeField] private Vector2 walkDelay;
    [SerializeField] private Vector2 idleDelay;

    [Space]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkStoppingDistance;

    private bool canWalkAround = true;
    private float walkDelayTimer;

    private Vector3 walkPoint;

    public override void OnInit()
    {
        base.OnInit();

        canWalkAround = false;
        Invoke(nameof(ResetWalkAround), Random.Range(idleDelay.x, idleDelay.y));
    }

    public override bool OnValidateState()
    {
        return canWalkAround;
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        walkPoint = transform.position + new Vector3(Random.Range(-walkableRadius / 2, walkableRadius / 2), 25,
                                                                 Random.Range(-walkableRadius / 2, walkableRadius / 2));

        if (Physics.Raycast(walkPoint, Vector3.down, out RaycastHit hit, 100, walkableLayers))
        {
            walkPoint.y = hit.point.y;

            agent.SetDestination(walkPoint);
        }

        agent.speed = walkSpeed;
        agent.stoppingDistance = walkStoppingDistance;
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();

        walkDelayTimer -= Time.deltaTime;

        if (walkDelayTimer <= 0 || Vector3.Distance(transform.position, walkPoint) <= 0.1f)
        {
            canWalkAround = false;
            Invoke(nameof(ResetWalkAround), Random.Range(idleDelay.x, idleDelay.y));
            walkDelayTimer = Random.Range(walkDelay.x, walkDelay.y);
        }
    }

    private void ResetWalkAround()
    {
        canWalkAround = true;
    }
}
