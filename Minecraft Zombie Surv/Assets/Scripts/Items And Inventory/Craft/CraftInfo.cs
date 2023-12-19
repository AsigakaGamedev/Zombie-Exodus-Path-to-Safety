using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Craft")]
public class CraftInfo : ScriptableObject 
{
    [SerializeField] private List<ItemData> creationPriceList;
    [SerializeField] private List<ItemData> createdItemsList;

    public List<ItemData> CreationPriceList { get => creationPriceList; }
    public List<ItemData> CreatedItemsList { get => createdItemsList; }
}
