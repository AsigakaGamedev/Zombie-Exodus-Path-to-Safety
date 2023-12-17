using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private int cellsCount = 24;

    [Space]
    [ReadOnly, SerializeField] private List<InventoryCellEntity> cells;

    public List<InventoryCellEntity> Cells { get => cells; }

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
}