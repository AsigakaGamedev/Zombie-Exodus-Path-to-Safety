using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Action<PlayerController> onInteract;

    public void Interact(PlayerController player)
    {
        onInteract?.Invoke(player);
    }
}
