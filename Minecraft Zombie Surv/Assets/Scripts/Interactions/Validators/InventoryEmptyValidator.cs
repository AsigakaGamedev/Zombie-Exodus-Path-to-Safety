using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEmptyValidator : AInteractValidator
{
    public override bool OnValidateInteract(PlayerController player)
    {
        return !player.Inventory.IsFull;
    }
}
