using System.Collections;
using UnityEngine;

public abstract class AInteractableComponent : MonoBehaviour
{
    [SerializeField] private InteractableObject interactableObject;

    private void OnValidate()
    {
        if (!interactableObject) interactableObject = GetComponent<InteractableObject>();
    }

    protected virtual void Start()
    {
        interactableObject.onInteract += OnInteract;
    }

    protected virtual void OnDestroy()
    {
        interactableObject.onInteract -= OnInteract;
    }

    protected abstract void OnInteract(PlayerController player);
}