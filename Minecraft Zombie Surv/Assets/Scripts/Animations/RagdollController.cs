using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private CharacterJoint[] joints;
    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private Collider[] colliders;

    [Space]
    [SerializeField] private Animator animator;

    [Button]
    public void Initialize()
    {
        joints = GetComponentsInChildren<CharacterJoint>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    [Button]
    public void Activate()
    {
        animator.enabled = false;

        foreach (Rigidbody body in rigidbodies)
            body.isKinematic = false;

        foreach (Collider collider in colliders)
            collider.enabled = true;
    }

    [Button]
    public void Deactivate()
    {
        animator.enabled = true;

        foreach (Rigidbody body in rigidbodies)
            body.isKinematic = true;

        foreach (Collider collider in colliders)
            collider.enabled = false;
    }
}
