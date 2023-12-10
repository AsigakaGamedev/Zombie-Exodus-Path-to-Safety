using System.Collections;
using UnityEngine;

[System.Serializable]
public class ItemEntity 
{
    [SerializeField] private ItemInfo infoPrefab;
    [SerializeField] private int amount;

    public ItemEntity(ItemInfo infoPrefab, int amount)
    {
        this.infoPrefab = infoPrefab;
        this.amount = amount;
    }

    public ItemInfo InfoPrefab { get => infoPrefab; }
    public int Amount { get => amount; set => amount = value; }
}