using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryCell : PoolableObject, IDropHandler
{
    [SerializeField] private UIMovableInventoryItem movableItem;
    [SerializeField] private TextMeshProUGUI itemAmount;

    private InventoryCellEntity entity;

    public UIMovableInventoryItem MovableItem { get => movableItem; }
    public InventoryCellEntity Entity { get => entity; }

    private void OnDisable()
    {
        if (entity != null)
        {
            entity.onItemChange -= OnSlotItemChange;
        }
    }

    public void SetValues(InventoryCellEntity entity)
    {
        if (entity != null)
        {
            entity.onItemChange -= OnSlotItemChange;
        }

        this.entity = entity;
        this.entity.onItemChange += OnSlotItemChange;
        
        UpdateVisual();

        movableItem.SetCell(this);
        movableItem.SetItem(this.entity.Item);
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UIMovableInventoryItem droppedItem))
        {
            ItemEntity transferItem = new ItemEntity(droppedItem.Cell.Entity.Item.InfoPrefab, droppedItem.Cell.Entity.Item.Amount);
            droppedItem.Cell.Entity.Item = this.entity.Item;
            droppedItem.OnEndDrag(null);
            this.entity.Item = transferItem;
        }
    }

    private void OnSlotItemChange(ItemEntity item)
    {
        movableItem.SetItem(item);
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (this.entity != null && this.entity.Item != null && this.entity.Item.Amount > 0)
        {
            itemAmount.text = this.entity.Item.Amount.ToString();
            itemAmount.gameObject.SetActive(true);
        }
        else
        {
            itemAmount.gameObject.SetActive(false);
        }
    }
}
