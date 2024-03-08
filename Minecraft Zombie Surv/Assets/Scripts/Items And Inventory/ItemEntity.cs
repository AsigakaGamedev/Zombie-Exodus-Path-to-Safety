using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class ItemEntity 
{
    [SerializeField] private ItemInfo infoPrefab;
    [SerializeField] private int amount;

    public Action onUse;
    public Action onEquip;
    public Action onItemIsOut; //Когда предмет закончился

    public ItemInfo InfoPrefab { get => infoPrefab; }
    public int Amount { get => amount; 
        set
        {
            amount = value;
            if (amount <= 0) onItemIsOut?.Invoke();
        }
    }

    public ItemEntity(ItemInfo infoPrefab, int amount)
    {
        this.infoPrefab = infoPrefab;
        this.amount = amount;
    }

    public void UseItem()
    {
        if (!infoPrefab.CanUse) return;

        onUse?.Invoke();

        Amount--;

        if (Amount == 0)
        {
            onItemIsOut?.Invoke();
        }
    }

    public void EquipItem()
    {
        if (!infoPrefab.CanEquip && !infoPrefab.IsWeapon) return;

        onEquip?.Invoke();
    }
}