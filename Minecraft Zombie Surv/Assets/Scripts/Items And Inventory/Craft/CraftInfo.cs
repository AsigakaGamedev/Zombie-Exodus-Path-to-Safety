using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CraftType
{
    Weapon,
    Food,
    Materials,
    Other
}

[CreateAssetMenu(menuName ="Craft")]
public class CraftInfo : ScriptableObject
{
    [ShowAssetPreview, SerializeField] private Sprite craftSprite;

    [Space]
    [SerializeField] private string craftNameKey;
    [SerializeField] private string craftDescKey;
    [SerializeField] private CraftType craftType;

    [Space]
    [SerializeField] private List<ItemData> creationPriceList;
    [SerializeField] private List<ItemData> createdItemsList;

    public Sprite CraftSprite { get => craftSprite; }

    public string CraftNameKey { get => craftNameKey; }
    public string CraftDescKey { get => craftDescKey; }
    public CraftType CraftType { get => craftType; }

    public List<ItemData> CreationPriceList { get => creationPriceList; }
    public List<ItemData> CreatedItemsList { get => createdItemsList; }

    [Button]
    public void Initialize()
    {
        craftNameKey = name + "_name";
        craftDescKey = name + "_desc";
    }
}
