using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStatesController : MonoBehaviour
{
    [SerializeField] private AIStateBase[] states;

    [Space]
    [SerializeField] private HealthComponent health;
    [SerializeField] private Collider mainCollider;

    [Space]
    [ReadOnly, SerializeField] private AIStateBase currentState;

    private void Start()
    {
        foreach (var state in states)
        {
            state.OnInit();
        }

        if (health) health.onDie += OnDie;
    }

    private void OnDestroy()
    {
        if (health) health.onDie -= OnDie;
    }

    private void OnDie()
    {
        enabled = false;

        GetComponent<NavMeshAgent>().enabled = false;
        mainCollider.enabled = false;
    }

    private void Update()
    {
        foreach (var state in states)
        {
            if (state.OnValidateState())
            {
                if (currentState)
                {
                    if (currentState != state)
                    {
                        currentState.OnExitState();
                    }
                    else
                    {
                        break;
                    }
                }

                currentState = state;
                currentState.OnEnterState();

                break;
            }
        }

        currentState.OnUpdateState();
    }
}
