using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayers;
    [SerializeField] private float interactRadius;

    [Space]
    [ReadOnly, SerializeField] private InteractableObject curInteractable;

    public Action onFindInteractable;
    public Action onLoseInteractable;

    public void CheckInteractions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRadius, interactLayers);

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

    public void InteractWithCurrent(PlayerController player)
    {
        if (!curInteractable) return;

        curInteractable.Interact(player);
        curInteractable.HideOutline();
        curInteractable = null;
        onLoseInteractable?.Invoke();
    }
}
