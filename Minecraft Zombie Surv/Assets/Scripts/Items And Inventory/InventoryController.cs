using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : AInventory
{
    [Header("Equipment And Weapon")]
    [SerializeField] private EquipmentSlot[] equipmentSlots;

    [Header("Main Cells")]
    [SerializeField] private int mainCellsCount = 12;

    [Header("Quick Cells")]
    [SerializeField] private int quickCellsCount = 5;

    [Space]
    [ReadOnly, SerializeField] private List<InventoryCellEntity> mainCells;
    [ReadOnly, SerializeField] private List<InventoryCellEntity> quickCells;

    public Action<ItemEntity> onItemUse;
    public Action<ItemEntity> onItemEquip;

    public override List<InventoryCellEntity> MainCells { get => mainCells; }
    public List<InventoryCellEntity> QuickCells { get => quickCells; }

    public bool IsFull => GetFreeCell() == null;

    public void Init()
    {
        mainCells = new List<InventoryCellEntity>();
        quickCells = new List<InventoryCellEntity>();

        for (int i = 0; i < mainCellsCount; i++)
        {
            InventoryCellEntity newMainCell = new InventoryCellEntity();
            newMainCell.onItemUse += OnItemUsed;
            newMainCell.onItemEquip += OnItemEquiped;
            mainCells.Add(newMainCell);
        }

        for (int i = 0; i < quickCellsCount; i++)
        {
            InventoryCellEntity newQuickCell = new InventoryCellEntity();
            quickCells.Add(newQuickCell);
        }
    }

    public void Destroy()
    {
        foreach (var cell in mainCells)
        {
            cell.onItemUse -= OnItemUsed;
            cell.onItemEquip -= OnItemEquiped;
        }
    }

    #region Items

    public void AddItem(ItemData data)
    {
        int dataItemAmount = data.RandomAmount;
        if (dataItemAmount <= 0) return;

        InventoryCellEntity targetCell = GetCell(data.Info);

        if (targetCell != null)
        {
            targetCell.Item.Amount += dataItemAmount;
        }
        else
        {
            targetCell = GetFreeCell();

            if (targetCell != null)
            {
                targetCell.Item = new ItemEntity(data.Info, dataItemAmount);
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

    public bool HasItems(ItemData[] checkingData)
    {
        foreach (ItemData check in checkingData)
        {
            InventoryCellEntity materialCell = GetCell(check.Info);

            if (materialCell == null || materialCell.Item.Amount < check.RandomAmount)
            {
                return false;
            }
        }

        return true;
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
            equipmentSlots[info.EquipmentSlotID].Equip(item);
            print("Предмет экипирован");
        }

        onItemEquip?.Invoke(item);
    }

    public EquipmentSlot GetEquipmentSlot(int index)
    {
        return equipmentSlots[index];
    }

    #endregion

    #region Cells

    public InventoryCellEntity GetCell(ItemInfo info)
    {
        foreach (InventoryCellEntity cell in mainCells)
            if (cell.Item != null && cell.Item.InfoPrefab == info) return cell;

        return null;
    }

    public InventoryCellEntity GetFreeCell()
    {
        foreach (InventoryCellEntity cell in mainCells)
            if (cell.Item == null) return cell;

        return null;
    }

    #endregion

    #region Crafting

    public void CraftItem(CraftInfo craftRecipe)
    {
        if (HasItems(craftRecipe.CreationPriceList.ToArray()))
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

    #endregion
}