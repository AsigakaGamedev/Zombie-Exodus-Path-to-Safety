using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : PoolableObject, IDropHandler
{
    [SerializeField] private UIMovableInventoryItem movableItem;

    private InventoryCellEntity entity;

    public UIMovableInventoryItem MovableItem { get => movableItem; }
    public InventoryCellEntity Entity { get => entity; }

    public void SetEntity(InventoryCellEntity entity)
    {
        if (entity != null)
        {
            entity.onItemChange -= OnSlotItemChange;
        }

        this.entity = entity;
        this.entity.onItemChange += OnSlotItemChange;

        movableItem.SetSlot(this);
        movableItem.SetItem(this.entity.Item);
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UIMovableInventoryItem droppedItem))
        {
            ItemEntity transferItem = new ItemEntity(droppedItem.Slot.Entity.Item.InfoPrefab, droppedItem.Slot.Entity.Item.Amount);
            droppedItem.Slot.Entity.Item = this.entity.Item;
            droppedItem.OnEndDrag(null);
            this.entity.Item = transferItem;
        }
    }

    private void OnSlotItemChange(ItemEntity item)
    {
        movableItem.SetItem(item);
    }
}
