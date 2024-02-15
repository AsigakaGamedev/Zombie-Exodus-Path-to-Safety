using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum WeaponType { Pistol, Rifle}

public class WeaponModel : EquipmentModel
{
    [SerializeField] private AttacksHandler attacksHandler;

    [Space]
    [SerializeField] private WeaponType weaponType;

    public WeaponType WeaponType { get => weaponType; }

    public void Init()
    {
        attacksHandler.Init();
    }

    public bool TryAttack()
    {
        return attacksHandler.TryAttack();
    }
}
