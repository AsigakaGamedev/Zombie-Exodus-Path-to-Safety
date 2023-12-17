using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventorySlot : PoolableObject
{
    [SerializeField] private UIMovableInventoryItem movableItem;

    private InventoryCellEntity entity;

    public UIMovableInventoryItem MovableItem { get => movableItem; }

    public void SetEntity(InventoryCellEntity entity)
    {
        if (entity != null)
        {
            entity.onItemChange -= OnSlotItemChange;
        }

        this.entity = entity;
        this.entity.onItemChange += OnSlotItemChange;

        movableItem.SetItem(this.entity.Item);
    }

    private void OnSlotItemChange(ItemEntity item)
    {
        movableItem.SetItem(item);
    }
}
