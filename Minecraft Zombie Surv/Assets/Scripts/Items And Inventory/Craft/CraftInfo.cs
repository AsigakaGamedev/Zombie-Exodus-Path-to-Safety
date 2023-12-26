using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CraftType
{
    Weapon,
    Block,
    Other
}

[CreateAssetMenu(menuName ="Craft")]
public class CraftInfo : ScriptableObject 
{
    [SerializeField] private List<ItemData> creationPriceList;
    [SerializeField] private List<ItemData> createdItemsList;
    [SerializeField] private string craftName;
    [SerializeField] private string craftDescription;
    [SerializeField] private CraftType craftType;

    public string CraftName
    {
        get => craftName;
    }

    public string CraftDescription
    {
        get => craftDescription;
    }

    public CraftType CraftType
    {
        get => craftType;
    }

    public List<ItemData> CreationPriceList { get => creationPriceList; }
    public List<ItemData> CreatedItemsList { get => createdItemsList; }
}
