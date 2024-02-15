using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : AInventory
{
    [Header("Caft")]
    [SerializeField] private CraftInfo[] allCrafts;

    [Header("Equipment And Weapon")]
    [SerializeField] private SerializedDictionary<int, EquipmentSlot> equipmentSlots;

    [Header("Cells")]
    [SerializeField] private int cellsCount = 24;

    [Space]
    [ReadOnly, SerializeField] private List<InventoryCellEntity> cells;

    public Action<ItemEntity> onItemUse;
    public Action<ItemEntity> onItemEquip;

    public override List<InventoryCellEntity> Cells { get => cells; }

    public CraftInfo[] AllCrafts { get => allCrafts; }

    public void Init()
    {
        cells = new List<InventoryCellEntity>();

        for (int i = 0; i < cellsCount; i++)
        {
            InventoryCellEntity newCell = new InventoryCellEntity();
            newCell.onItemUse += OnItemUsed;
            newCell.onItemEquip += OnItemEquiped;
            cells.Add(newCell);
        }
    }

    public void Destroy()
    {
        foreach (var cell in cells)
        {
            cell.onItemUse -= OnItemUsed;
            cell.onItemEquip -= OnItemEquiped;
        }
    }

    #region Items

    public void AddItem(ItemData data)
    {
        InventoryCellEntity targetCell = GetCell(data.Info);

        if (targetCell != null)
        {
            targetCell.Item.Amount += data.RandomAmount;
        }
        else
        {
            targetCell = GetFreeCell();

            if (targetCell != null)
            {
                targetCell.Item = new ItemEntity(data.Info, data.RandomAmount);
            }
        }
    }

    public int GetItemsAmount(ItemInfo itemInfo)
    {
        InventoryCellEntity itemCell = GetCell(itemInfo);

        if (itemCell != null)
        {
            if (itemCell.Item != null && itemCell.Item.InfoPrefab != null)
            {
                return itemCell.Item.Amount;
            }
            else
            {
                return 0;
            }
        }

        return 0;
    }

    public void OnItemUsed(ItemEntity item)
    {
        onItemUse?.Invoke(item);
    }

    #endregion

    #region Equipment And Weapon

    private void OnItemEquiped(ItemEntity item)
    {
        ItemInfo info = item.InfoPrefab;

        if (info.IsWeapon)
        {
            print("Оружие экипировано");
        }
        else
        {
            print("Предмет экипирован");
        }

        onItemEquip?.Invoke(item);
    }

    #endregion

    #region Cells

    public InventoryCellEntity GetCell(ItemInfo info)
    {
        foreach (InventoryCellEntity cell in cells)
            if (cell.Item != null && cell.Item.InfoPrefab == info) return cell;

        return null;
    }

    public InventoryCellEntity GetFreeCell()
    {
        foreach (InventoryCellEntity cell in cells)
            if (cell.Item == null) return cell;

        return null;
    }

    #endregion

    #region Crafting

    public void CraftItem(CraftInfo craftRecipe)
    {
        if (CanCraftItem(craftRecipe))
        {
            foreach (ItemData material in craftRecipe.CreationPriceList)
            {
                InventoryCellEntity materialCell = GetCell(material.Info);
                materialCell.Item.Amount -= material.RandomAmount;
            }

            foreach (ItemData createdItem in craftRecipe.CreatedItemsList)
            {
                AddItem(createdItem);
            }
        }
        else
        {
            Debug.LogWarning("Not enough materials to craft item!");
        }
    }

    private bool CanCraftItem(CraftInfo craftRecipe)
    {
        foreach (ItemData material in craftRecipe.CreationPriceList)
        {
            InventoryCellEntity materialCell = GetCell(material.Info);

            if (materialCell == null || materialCell.Item.Amount < material.RandomAmount)
            {
                return false;
            }
        }

        return true;
    }

    #endregion
}