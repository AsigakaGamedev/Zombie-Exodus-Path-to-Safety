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
        interactableObject.onSuccessInteract += OnSuccessInteract;
        interactableObject.onFailedInteract += OnFailedInteract;
    }

    protected virtual void OnDestroy()
    {
        interactableObject.onSuccessInteract -= OnSuccessInteract;
        interactableObject.onFailedInteract -= OnFailedInteract;
    }

    protected virtual void OnSuccessInteract(PlayerController player) { }
    protected virtual void OnFailedInteract(PlayerController player) { }
}