using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip openClip;
    [SerializeField] private AnimationClip closeClip;

    [Space]
    [SerializeField] private bool isOpened;

    [Space]
    [SerializeField] private float switchStateDelay = 1;

    private bool canSwtichState;

    private void Start()
    {
        canSwtichState = true;

        if (isOpened)
            Open();
        else
            Close();
    }

    [Button]
    public void Open()
    {
        if (isOpened || !canSwtichState) return;

        anim.clip = openClip;
        anim.Play();
        isOpened = true;

        canSwtichState = false;
        Invoke(nameof(ResetCanSwitchState), switchStateDelay);
    }

    [Button]
    public void Close()
    {
        if (!isOpened || !canSwtichState) return;

        anim.clip = closeClip;
        anim.Play();
        isOpened = false;

        canSwtichState = false;
        Invoke(nameof(ResetCanSwitchState), switchStateDelay);
    }

    [Button]
    public void SwitchState()
    {
        if (isOpened)
            Close();
        else
            Open();
    }

    private void ResetCanSwitchState()
    {
        canSwtichState = true;
    }
}
