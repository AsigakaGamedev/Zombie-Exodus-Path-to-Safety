using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemInteract : AInteractValidator
{
    [Space]
    [SerializeField] private ItemData itemData;

    public override bool OnValidateInteract(PlayerController player)
    {
        return player.Inventory.TryAddItem(itemData);
    }
}
