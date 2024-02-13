using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMovableInventoryItem : UIMovableObject
{
    private UIInventoryCell slot;

    protected ItemEntity item;

    public UIInventoryCell Slot { get => slot; }

    public void SetSlot(UIInventoryCell slot)
    {
        this.slot = slot;
    }

    public void SetItem(ItemEntity item)
    {
        this.item = item;

        iconImg.gameObject.SetActive(item != null && item.InfoPrefab);

        if (item != null && item.InfoPrefab)
        {
            iconImg.sprite = item.InfoPrefab.ItemCellSprite;
        }
    }
}
