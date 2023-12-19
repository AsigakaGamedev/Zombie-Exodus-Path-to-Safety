using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private CraftInfo[] allCrafts;

    [Space]
    [SerializeField] private int cellsCount = 24;

    [Space]
    [ReadOnly, SerializeField] private List<InventoryCellEntity> cells;

    public List<InventoryCellEntity> Cells { get => cells; }
    public CraftInfo[] AllCrafts { get => allCrafts; }

    public void Init()
    {
        cells = new List<InventoryCellEntity>();

        for (int i = 0; i < cellsCount; i++)
        {
            InventoryCellEntity newCell = new InventoryCellEntity();
            cells.Add(newCell);
        }
    }

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

    public void CraftItem(CraftInfo craftRecipe)
    {
        // Проверить, достаточно ли материалов для крафта
        if (CanCraftItem(craftRecipe))
        {
            // Уменьшить количество материалов в инвентаре
            foreach (ItemData material in craftRecipe.CreationPriceList)
            {
                InventoryCellEntity materialCell = GetCell(material.Info);
                materialCell.Item.Amount -= material.RandomAmount;
            }

            // Добавить созданные предметы в инвентарь
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
                // Не хватает материалов для крафта
                return false;
            }
        }

        // Достаточно материалов для крафта
        return true;
    }



}