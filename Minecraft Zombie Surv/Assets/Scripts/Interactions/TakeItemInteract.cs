using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemInteract : MonoBehaviour
{
    [SerializeField] private InteractableObject interactableObject;

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
        print("Take Item");
    }
}
