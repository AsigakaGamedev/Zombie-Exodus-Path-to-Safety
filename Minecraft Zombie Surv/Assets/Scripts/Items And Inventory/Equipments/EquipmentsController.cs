using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<int, EquipmentSlot> slots;

    public void TryEquip(Equipable equipable)
    {
        slots[equipable.EquipmentSlotID].Equip(equipable.EquipmentID);
    }
}
