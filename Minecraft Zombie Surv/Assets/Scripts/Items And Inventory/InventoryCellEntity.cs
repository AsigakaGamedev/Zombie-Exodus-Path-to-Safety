using System.Collections;
using UnityEngine;

[System.Serializable]
public class InventoryCellEntity 
{
    [SerializeField] private ItemEntity item;

    public ItemEntity Item { get => item; set => item = value; }
}