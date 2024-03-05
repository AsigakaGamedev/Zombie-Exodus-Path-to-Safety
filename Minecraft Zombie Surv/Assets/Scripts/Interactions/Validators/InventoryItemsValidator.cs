using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsValidator : AInteractValidator
{
    [SerializeField] private ItemData[] neededItems;

    [Space]
    [SerializeField] private bool removeItemsOnInteract;

    public override bool OnValidateInteract(PlayerController player)
    {
        return player.Inventory.HasItems(neededItems);
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        base.OnSuccessInteract(player);

        if (removeItemsOnInteract)
        {
            //Óהאכÿול ןנוהלועû
        }
    }
}
