using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private Sprite itemCellSprite;

    [Space]
    [SerializeField] private string itemNameKey;
    [SerializeField] private string itemDescriptionKey;

    [Space]
    [HideIf(nameof(isWeapon)), SerializeField] private bool canEquip;
    [ShowIf(nameof(canEquip)), SerializeField] private int equipmentSlotID;
    [ShowIf(nameof(canEquip)), SerializeField] private string equipmentModelID;

    [Space]
    [HideIf(nameof(canEquip)), SerializeField] private bool isWeapon;
    [ShowIf(nameof(isWeapon)), SerializeField] private int weaponID;

    [Space]
    [SerializeField] private bool canUse;
    [ShowIf(nameof(canUse)), SerializeField] private ItemUseData[] useDatas;
    [ShowIf(nameof(canUse)), SerializeField] private float healthIncrease;

    public Sprite ItemCellSprite { get => itemCellSprite; }
    public string ItemNameKey { get => itemNameKey; }
    public string ItemDescriptionKey { get => itemDescriptionKey; }

    public bool CanEquip { get => canEquip; }
    public int EquipmentSlotID { get => equipmentSlotID; }
    public string EquipmentModelID { get => equipmentModelID; }

    public bool IsWeapon { get => isWeapon; }
    public int WeaponID { get => weaponID; }

    public bool CanUse { get => canUse; }
    public ItemUseData[] UseDatas { get => useDatas; }
    public float HealthIncrease { get => healthIncrease; }
}

[System.Serializable]
public class ItemUseData
{
    [SerializeField] private string needID;
    [SerializeField] private float needIncreaseValue;

    public string NeedID { get => needID; }
    public float NeedIncreaseValue { get => needIncreaseValue; }
}