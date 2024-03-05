using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemInteract : AInteractableComponent
{
    [Space]
    [SerializeField] private ItemData itemData;

    protected override void OnSuccessInteract(PlayerController player)
    {
        player.Inventory.AddItem(itemData);
    }
}
