using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuickOutline))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool deactivateOnInteract = true;

    [Space]
    [SerializeField] private QuickOutline outline;

    public Action<PlayerController> onInteract;

    private void OnValidate()
    {
        if (!outline) outline = GetComponent<QuickOutline>();
    }

    private void Start()
    {
        HideOutline();
    }

    public void ShowOutline()
    {
        outline.enabled = true;
    }

    public void HideOutline()
    {
        outline.enabled = false;
    }

    public void Interact(PlayerController player)
    {
        onInteract?.Invoke(player);

        if (deactivateOnInteract) gameObject.SetActive(false);
    }
}
