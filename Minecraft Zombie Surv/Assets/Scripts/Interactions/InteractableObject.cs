using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(QuickOutline))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private AudioClip successInteractAudio;
    [SerializeField] private AudioClip failedInteractAudio;
    [SerializeField] private bool deactivateOnInteract = true;
    [SerializeField] private UnityEvent successInteractEvent;
    [SerializeField] private UnityEvent failedInteractEvent;


    [Space]
    [SerializeField] private QuickOutline outline;

    [Space]
    [SerializeField] private AInteractValidator[] validators; 

    public Action<PlayerController> onSuccessInteract;
    public Action<PlayerController> onFailedInteract;

    [Space]
    private AudioManager audioManager;

    private void OnValidate()
    {
        if (!outline) outline = GetComponent<QuickOutline>();
    }

    [Inject]
    private void Construct(AudioManager audioManager)
    {
        this.audioManager = audioManager;
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

    public void TryInteract(PlayerController player)
    {
        if (CanInteract(player))
        {
            onSuccessInteract?.Invoke(player);
            successInteractEvent.Invoke();

            if (successInteractAudio) audioManager.PlayAudio(successInteractAudio, AudioType.Effects);

            if (deactivateOnInteract) gameObject.SetActive(false);
        }
        else
        {
            if (failedInteractAudio) audioManager.PlayAudio(failedInteractAudio, AudioType.Effects);

            onFailedInteract?.Invoke(player);
            failedInteractEvent?.Invoke();
        }
    }

    public bool CanInteract(PlayerController player)
    {
        foreach (AInteractValidator validator in validators)
        {
            if (!validator.OnValidateInteract(player)) return false;
        }

        return true;
    }
}
