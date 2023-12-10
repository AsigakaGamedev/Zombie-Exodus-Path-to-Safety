using System.Collections;
using UnityEngine;

[System.Serializable]
public struct ItemData 
{
    [SerializeField] private ItemInfo info;
    [SerializeField] private Vector2Int amount;

    public ItemInfo Info { get => info; }
    public int RandomAmount { get => Random.Range(amount.x, amount.y); }
}