using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UIPlayerEquipmentSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int equipmentSlotIndex;

    [Space]
    [SerializeField] private Image emptySlotImg;
    [SerializeField] private GameObject showedItem;

    private PlayerController playerController;
    private EquipmentSlot linkedSlot;

    [Inject]
    private void Construct(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void Start()
    {
        linkedSlot = playerController.Inventory.GetEquipmentSlot(equipmentSlotIndex);
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
