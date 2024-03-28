using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIInventoryPanel : MonoBehaviour
{
    [SerializeField] private UIInventoryCell cellPrefab;
    [SerializeField] private Transform cellsContent;

    private ObjectPoolingManager poolingManager;

    private List<UIInventoryCell> spawnedSlots = new List<UIInventoryCell>();

    [Inject]
    private void Construct(ObjectPoolingManager poolingManager)
    {
        this.poolingManager = poolingManager;
    }

    public void OpenList(List<InventoryCellEntity> cells)
    {
        foreach (UIInventoryCell spawnedSlot in spawnedSlots)
        {
            spawnedSlot.gameObject.SetActive(false);
        }

        spawnedSlots.Clear();

        foreach (InventoryCellEntity slotEntity in cells)
        {
            UIInventoryCell newSlot = poolingManager.GetPoolable(cellPrefab);
            newSlot.transform.SetParent(cellsContent);
            newSlot.transform.localScale = Vector3.one;
            newSlot.SetValues(slotEntity);
            newSlot.MovableItem.SetParents(newSlot.transform, transform.parent);
            spawnedSlots.Add(newSlot);
        }
    }
}