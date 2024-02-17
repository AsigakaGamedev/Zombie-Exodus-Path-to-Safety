using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, EquipmentModel> models;

    [Space]
    [ReadOnly, SerializeField] private EquipmentModel equipedModel;
    
    private ItemEntity equipedItem;

    public Action<ItemEntity> onEquipedItemChange;

    public ItemEntity EquipedItem { get => equipedItem; }

    public void Equip(ItemEntity item)
    {
        ItemInfo info = item.InfoPrefab;

        if (!info.CanEquip) return;

        equipedItem = item;
        onEquipedItemChange?.Invoke(item);

        if (!models.ContainsKey(info.EquipmentModelID)) return;

        DequipCurrentModel();

        equipedModel = models[info.EquipmentModelID];
        equipedModel.OnEquip();
    }

    private void DequipCurrentModel()
    {
        equipedItem = null;
        onEquipedItemChange?.Invoke(equipedItem);

        if (!equipedModel) return;

        equipedModel.OnDequip();
        equipedModel = null;
    }
}
