using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuickCellsPanel : MonoBehaviour
{
    [SerializeField] private UIQuickCell quickCellPrefab;
    [SerializeField] private Transform cellsContent;

    private InventoryController inventory;
    private UIQuickCell spawnedCells;
}
