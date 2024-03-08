using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum WeaponType { Pistol, Rifle, Axe}

public class WeaponModel : EquipmentModel
{
    [SerializeField] private AttacksHandler attacksHandler;
    [SerializeField] private AudioClip attackAudio;

    [Space]
    [SerializeField] private string animTriggerKey;

    [Header("Ammo & Reloading")]
    [SerializeField] private bool needAmmo;
    [ShowIf(nameof(needAmmo)), SerializeField] private float reloadingTime;
    [ShowIf(nameof(needAmmo)), SerializeField] private AudioClip reloadingAudio;

    [Space]
    [ShowIf(nameof(needAmmo)), SerializeField] private int magazineCapacity = 8;
    [ShowIf(nameof(needAmmo)), SerializeField] private ItemInfo ammoInfo;

    [Space]
    [ShowIf(nameof(needAmmo)), ReadOnly, SerializeField] private int ammoInMagazine;

    private AudioManager audioManager;
    private bool reloadingInProcess;

    public Action onReloadEnd;

    public bool NeedAmmo { get => needAmmo; }
    public ItemInfo AmmoInfo { get => ammoInfo; }
    public int AmmoInMagazine { get => ammoInMagazine; }

    public string AnimKey { get => animTriggerKey;}

    public void Init()
    {
        reloadingInProcess = false;
        attacksHandler.Init();

        if (attackAudio || reloadingAudio) audioManager = ServiceLocator.GetServiceSafe<AudioManager>(); 
    }

    public bool TryAttack()
    {
        if (needAmmo)
        {
            if (reloadingInProcess)
            {
                return false;
            }

            if (ammoInMagazine <= 0)
            {
                TryStartReload();
                return false;
            }
        }

        if (!attacksHandler.TryAttack()) return false;

        if (needAmmo)
        {
            ammoInMagazine--;
        }

        if (attackAudio && audioManager) audioManager.PlayAudio(attackAudio, AudioType.Effects);

        return true;
    }

    public bool TryStartReload()
    {
        if (reloadingAudio && audioManager) audioManager.PlayAudio(reloadingAudio, AudioType.Effects);

        reloadingInProcess = true;
        Invoke(nameof(EndReload), reloadingTime);

        return needAmmo;
    }

    private void EndReload()
    {
        ammoInMagazine = magazineCapacity;
        reloadingInProcess = false;
        onReloadEnd?.Invoke();
    }
}
