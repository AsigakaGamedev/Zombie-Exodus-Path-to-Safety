using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour
{
    [SerializeField] private int equipmentSlotID;
    [SerializeField] private string equipmentID;

    public int EquipmentSlotID { get => equipmentSlotID; }
    public string EquipmentID { get => equipmentID; }
}
