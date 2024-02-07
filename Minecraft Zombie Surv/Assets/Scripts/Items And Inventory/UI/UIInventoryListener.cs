using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryListener : MonoBehaviour
{
    [SerializeField] private UIInventoryPanel panel;
    [SerializeField] private AInventory inventory;

    private void OnEnable()
    {
        panel.OpenList(inventory.Cells);
    }
}