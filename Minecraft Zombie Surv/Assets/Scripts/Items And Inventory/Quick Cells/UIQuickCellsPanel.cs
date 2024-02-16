using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuickCellsPanel : MonoBehaviour
{
    [SerializeField] private UIQuickCell quickCellPrefab;
    [SerializeField] private Transform cellsContent;

    private InventoryController inventory;
    private UIQuickCell spawnedCells;

    private void Start()
    {
        inventory = ServiceLocator.GetService<PlayerController>().Inventory;
    }

    private void UpdatePanel()
    {

    }
}
