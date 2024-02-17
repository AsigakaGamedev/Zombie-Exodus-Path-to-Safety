using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlayerEquipmentSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int equipmentSlotIndex;

    [Space]
    [SerializeField] private Image emptySlotImg;
    [SerializeField] private GameObject showedItem;

    private EquipmentSlot linkedSlot;

    private void Start()
    {
        linkedSlot = ServiceLocator.GetService<PlayerController>().Inventory.GetEquipmentSlot(equipmentSlotIndex);
        linkedSlot.onEquipedItemChange += OnEquipedItemChange;
        UpdateVisual();
    }

    private void OnDestroy()
    {
        linkedSlot.onEquipedItemChange -= OnEquipedItemChange;
    }

    private void UpdateVisual()
    {
        emptySlotImg.enabled = linkedSlot.EquipedItem == null;
        showedItem.SetActive(linkedSlot.EquipedItem != null);
    }

    private void OnEquipedItemChange(ItemEntity item)
    {
        UpdateVisual();
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UIMovableInventoryItem movableItem) &&
            movableItem.Item.InfoPrefab.CanEquip && movableItem.Item.InfoPrefab.EquipmentSlotID == equipmentSlotIndex)
        {
            linkedSlot.Equip(movableItem.Item);
            UpdateVisual();
        }
    }
}
