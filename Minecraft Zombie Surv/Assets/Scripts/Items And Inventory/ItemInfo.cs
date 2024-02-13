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
    [SerializeField] private bool canEquip;
    [ShowIf(nameof(canEquip)), SerializeField] private int equipmentSlotID;

    [Space]
    [SerializeField] private bool canUse;

    public Sprite ItemCellSprite { get => itemCellSprite; }
    public string ItemNameKey { get => itemNameKey; }
    public string ItemDescriptionKey { get => itemDescriptionKey; }

    public bool CanEquip { get => canEquip; }
    public int EquipmentSlotID { get => equipmentSlotID; }

    public bool CanUse { get => canUse; }
}