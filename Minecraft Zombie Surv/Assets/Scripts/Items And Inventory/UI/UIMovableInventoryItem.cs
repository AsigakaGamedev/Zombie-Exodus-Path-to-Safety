using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMovableInventoryItem : UIMovableObject
{
    private UIInventoryCell cell;

    protected ItemEntity item;

    public UIInventoryCell Cell { get => cell; }

    public void SetCell(UIInventoryCell cell)
    {
        this.cell = cell;
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
