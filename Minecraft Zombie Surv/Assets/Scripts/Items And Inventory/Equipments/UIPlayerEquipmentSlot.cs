using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerEquipmentSlot : MonoBehaviour
{
    [SerializeField] private int equipmentIndex;

    [Space]
    [SerializeField] private Image emptySlotImg;
    [SerializeField] private GameObject showedItem;

    private EquipmentSlot linkedSlot;

    private void Start()
    {
        linkedSlot = ServiceLocator.GetService<PlayerController>().Inventory.GetEquipmentSlot(equipmentIndex);
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        emptySlotImg.enabled = linkedSlot.EquipedItem == null;
        showedItem.SetActive(linkedSlot.EquipedItem != null);
    }
}
