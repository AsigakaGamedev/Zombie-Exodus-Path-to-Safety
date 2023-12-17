using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class InventoryCellEntity 
{
    [SerializeField] private ItemEntity item;

    public Action<ItemEntity> onItemChange;

    public ItemEntity Item { get => item;
        set
        {
            item = value;
            onItemChange?.Invoke(item);
        }
    }
}