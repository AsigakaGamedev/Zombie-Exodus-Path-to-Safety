using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatesController : MonoBehaviour
{
    [SerializeField] private AIStateBase[] states;

    [Space]
    [ReadOnly, SerializeField] private AIStateBase currentState;

    private void Start()
    {
        foreach (var state in states)
        {
            state.OnInit();
        }

        currentState = states[0];
    }

    private void Update()
    {
        foreach (var state in states)
        {
            if (state.OnValidateState())
            {
                if (currentState != state)
                {
                    currentState.OnExitState();
                }
                else
                {
                    break;
                }

                currentState = state;
                currentState.OnEnterState();

                break;
            }
        }

        currentState.OnUpdateState();
    }
}
