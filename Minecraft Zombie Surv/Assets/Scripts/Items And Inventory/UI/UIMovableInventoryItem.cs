using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMovableInventoryItem : UIMovableObject
{
    private ItemEntity item;

    public void SetItem(ItemEntity item)
    {
        this.item = item;

        iconImg.gameObject.SetActive(item != null);

        if (item != null)
        {
            iconImg.sprite = item.InfoPrefab.ItemCellSprite;
        }
    }
}
