using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInventory : MonoBehaviour
{
    public abstract List<InventoryCellEntity> MainCells { get; }
}
