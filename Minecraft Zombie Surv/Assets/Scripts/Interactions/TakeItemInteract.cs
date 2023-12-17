using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemInteract : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    [Space]
    [SerializeField] private InteractableObject interactableObject;

    private void OnValidate()
    {
        if (!interactableObject) interactableObject = GetComponent<InteractableObject>();
    }

    private void Start()
    {
        interactableObject.onInteract += OnInteract;
    }

    private void OnDestroy()
    {
        interactableObject.onInteract -= OnInteract;
    }

    private void OnInteract(PlayerController player)
    {
        player.Inventory.AddItem(itemData);
    }
}
