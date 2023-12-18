using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftInfo
{
    [SerializeField] private List<ItemData> creationPriceList;
    [SerializeField] private List<ItemData> createdItemsList;

    public List<ItemData> CreationPriceList { get => creationPriceList; }
    public List<ItemData> CreatedItemsList { get => createdItemsList; }
}
