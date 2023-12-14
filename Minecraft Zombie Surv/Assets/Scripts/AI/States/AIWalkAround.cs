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

    [Space]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkStoppingDistance;

    private float walkDelayTimer;

    public override void OnEnterState()
    {
        base.OnEnterState();

        agent.speed = walkSpeed;
        agent.stoppingDistance = walkStoppingDistance;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();

        walkDelayTimer -= Time.deltaTime;

        if (walkDelayTimer <= 0)
        {
            walkDelayTimer = Random.Range(walkDelay.x, walkDelay.y);

            Vector3 walkPoint = transform.position + new Vector3(Random.Range(-walkableRadius / 2, walkableRadius / 2), 25,
                                                                 Random.Range(-walkableRadius / 2, walkableRadius / 2));

            if (Physics.Raycast(walkPoint, Vector3.down, out RaycastHit hit, 100, walkableLayers))
            {
                walkPoint.y = hit.point.y;

                agent.SetDestination(walkPoint);
            }
        }
    }
}
