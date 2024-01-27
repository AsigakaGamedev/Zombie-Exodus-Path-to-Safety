using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private int startWeaponID = -1;
    [SerializeField] private SerializedDictionary<int, Weapon> allWeapons;

    [Space]
    [ReadOnly, SerializeField] private Weapon weaponInHands;

    public Action<Weapon> onEquip;
    public Action onDequip;

    public void Init()
    {
        if (startWeaponID != -1) EquipWeapon(startWeaponID);

        foreach (var weapon in allWeapons.Values)
        {
            weapon.Init();
        }
    }

    public bool TryAttack()
    {
        if (weaponInHands == null) return false;

        return weaponInHands.TryAttack();
    }

    public void EquipWeapon(int weaponID)
    {
        if (weaponInHands) onDequip?.Invoke(); ;

        weaponInHands = allWeapons[weaponID];
        weaponInHands.OnEquip();
        onEquip?.Invoke(weaponInHands);
        onDequip += weaponInHands.OnDequip;
        onDequip += DequipWeapon;
    }

    public void DequipWeapon()
    {
        onDequip -= weaponInHands.OnDequip;
        onDequip -= DequipWeapon;
    }
}
