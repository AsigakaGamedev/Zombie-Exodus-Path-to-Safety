using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentModel : MonoBehaviour
{
    public void OnEquip()
    {
        gameObject.SetActive(true);
    }

    public void OnDequip()
    {
        gameObject.SetActive(false);
    }
}
