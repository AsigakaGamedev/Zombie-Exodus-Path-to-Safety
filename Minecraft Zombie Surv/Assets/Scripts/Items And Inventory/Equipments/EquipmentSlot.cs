using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, EquipmentModel> models;

    [Space]
    [ReadOnly, SerializeField] private EquipmentModel equipedModel;
    [ReadOnly, SerializeField] private ItemEntity equipedItem;

    public void Equip(string equipmentID)
    {
        if (!models.ContainsKey(equipmentID)) return;

        DequipCurrentModel();

        equipedModel = models[equipmentID];
        equipedModel.OnEquip();
    }

    private void DequipCurrentModel()
    {
        if (!equipedModel) return;

        equipedModel.OnDequip();
        equipedModel = null;
    }
}
