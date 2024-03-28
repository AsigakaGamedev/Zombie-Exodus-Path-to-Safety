using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemsContainerInteract : AInteractableComponent
{
    [SerializeField] private ItemData[] spawnableItems;

    [Space]
    [SerializeField] private int cellsCount;

    [Space]
    [ReadOnly, SerializeField] private List<InventoryCellEntity> containerCells;

    private UIManager uiManager;
    private UIInventoriesManager uiInventories;

    [Inject]
    private void Construct(UIManager uiManager, UIInventoriesManager uiInventories)
    {
        this.uiManager = uiManager;
        this.uiInventories = uiInventories;
    }

    protected override void Start()
    {
        base.Start();

        containerCells = new List<InventoryCellEntity>();

        for (int i = 0; i < cellsCount; i++)
        {
            InventoryCellEntity newCell = new InventoryCellEntity();
            containerCells.Add(newCell);
        }

        int itemCellIndex = 0;

        foreach (ItemData spawnItem in  spawnableItems)
        {
            int dataItemAmount = spawnItem.RandomAmount;
            if (dataItemAmount <= 0) continue;

            ItemEntity newItem = new ItemEntity(spawnItem.Info, dataItemAmount);
            containerCells[itemCellIndex].Item = newItem;
            itemCellIndex++;
        }
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        uiInventories.OpenPanel(0, containerCells);
        uiManager.ChangeScreen("container");
    }
}