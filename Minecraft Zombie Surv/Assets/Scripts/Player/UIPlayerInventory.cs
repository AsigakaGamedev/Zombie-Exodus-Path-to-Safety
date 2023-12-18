using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInventory : MonoBehaviour
{
    [SerializeField] private UIInventorySlot slotPrefab;
    [SerializeField] private Transform itemsContent;

    private ObjectPoolingManager poolingManager;
    private LevelContoller levelContoller;
    private PlayerController player;

    private List<UIInventorySlot> spawnedSlots;

    private void Start()
    {
        spawnedSlots = new List<UIInventorySlot>();

        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();
        levelContoller = ServiceLocator.GetService<LevelContoller>();
    }

    private void OnEnable()
    {
        if (!player)
        {
            player = levelContoller.PlayerInstance;

            if (!player) return;

            foreach (InventoryCellEntity slotEntity in player.Inventory.Cells)
            {
                UIInventorySlot newSlot = poolingManager.GetPoolable(slotPrefab);
                newSlot.transform.SetParent(itemsContent);
                newSlot.SetEntity(slotEntity);
                newSlot.MovableItem.SetParents(newSlot.transform, transform);
                spawnedSlots.Add(newSlot);
            }
        }
    }
}
