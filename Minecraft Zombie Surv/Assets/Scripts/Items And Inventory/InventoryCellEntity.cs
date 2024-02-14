﻿using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class InventoryCellEntity 
{
    [SerializeField] private ItemEntity item;

    public Action<ItemEntity> onItemChange;
    public Action<ItemEntity> onItemUse;

    public ItemEntity Item { get => item;
        set
        {
            if (item != null)
            {
                item.onUse -= OnItemUse;
                item.onItemIsOut -= OnItemIsOut;
            }

            item = value;
            onItemChange?.Invoke(item);

            if (item != null)
            {
                item.onUse += OnItemUse;
                item.onItemIsOut += OnItemIsOut;
            }
        }
    }

    private void OnItemUse()
    {
        onItemUse?.Invoke(item);
    }

    private void OnItemIsOut()
    {
        item = null;
        onItemChange?.Invoke(item);
    }
}