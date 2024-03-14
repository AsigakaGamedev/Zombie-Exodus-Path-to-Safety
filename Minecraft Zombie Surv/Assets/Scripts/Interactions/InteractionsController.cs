using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayers;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float interactDistance;

    [Space]
    [SerializeField] private Transform checkPoint;

    [Space]
    [ReadOnly, SerializeField] private InteractableObject curInteractable;

    public Action onFindInteractable;
    public Action onLoseInteractable;

    public void CheckInteractions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance, interactLayers);

        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out InteractableObject interactable))
                {
                    if (curInteractable && curInteractable != interactable)
                    {
                        curInteractable.HideOutline();
                    }

                    curInteractable = interactable;
                    curInteractable.ShowOutline();
                    onFindInteractable?.Invoke();
                    return;
                }
            }
        }

        if (curInteractable)
        {
            curInteractable.HideOutline();
            curInteractable = null;
            onLoseInteractable?.Invoke();
        }
    }

    public void CheckInteractionsFront()
    {
        Ray attackRay = new Ray(checkPoint.position, checkPoint.forward);

        if (Physics.Raycast(attackRay, out RaycastHit hit, interactDistance, interactLayers) && 
            !Physics.Raycast(attackRay, Vector3.Distance(transform.position, hit.point) - 0.5f, obstacleLayers))
        {
            if (hit.collider.TryGetComponent(out InteractableObject interactable))
            {
                if (curInteractable && curInteractable != interactable)
                {
                    curInteractable.HideOutline();
                }

                curInteractable = interactable;
                curInteractable.ShowOutline();
                onFindInteractable?.Invoke();
                return;
            }
        }

        if (curInteractable)
        {
            curInteractable.HideOutline();
            curInteractable = null;
            onLoseInteractable?.Invoke();
        }
    }

    public void InteractWithCurrent(PlayerController player)
    {
        if (!curInteractable) return;

        curInteractable.TryInteract(player);
        curInteractable.HideOutline();
        curInteractable = null;
        onLoseInteractable?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * interactDistance);
    }


}
