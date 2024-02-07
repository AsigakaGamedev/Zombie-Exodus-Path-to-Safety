using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsContainerInteract : AInteractableComponent
{
    [SerializeField] private ItemData[] spawnableItems;

    [Space]
    [SerializeField] private int cellsCount;

    [Space]
    [ReadOnly, SerializeField] private List<InventoryCellEntity> containerCells;

    private UIManager uiManager;
    private UIInventoriesManager uiInventories;

    protected override void Start()
    {
        base.Start();

        uiManager = ServiceLocator.GetService<UIManager>();
        uiInventories = ServiceLocator.GetService<UIInventoriesManager>();

        containerCells = new List<InventoryCellEntity>();

        for (int i = 0; i < cellsCount; i++)
        {
            InventoryCellEntity newCell = new InventoryCellEntity();
            containerCells.Add(newCell);
        }

        int itemCellIndex = 0;

        foreach (ItemData spawnItem in  spawnableItems)
        {
            ItemEntity newItem = new ItemEntity(spawnItem.Info, spawnItem.RandomAmount);
            containerCells[itemCellIndex].Item = newItem;
            itemCellIndex++;
        }
    }

    protected override void OnInteract(PlayerController player)
    {
        uiInventories.OpenPanel(0, containerCells);
        uiManager.ChangeScreen("container");
    }
}