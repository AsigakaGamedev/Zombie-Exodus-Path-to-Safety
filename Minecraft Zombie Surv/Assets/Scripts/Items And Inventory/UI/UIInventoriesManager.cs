using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoriesManager : MonoBehaviour
{
    [SerializeField] private UIInventoryPanel[] panels; 

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void OpenPanel(int panelIndex, List<InventoryCellEntity> cells)
    {
        panels[panelIndex].OpenList(cells);
    }
}
