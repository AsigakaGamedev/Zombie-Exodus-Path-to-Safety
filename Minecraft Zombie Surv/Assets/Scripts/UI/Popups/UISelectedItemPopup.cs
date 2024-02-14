using System.Collections;
using System.Collections.Generic;
using Unity.Services.Economy.Model;
using UnityEngine;
using UnityEngine.UI;

public class UISelectedItemPopup : APopup
{
    [SerializeField] private Button useItemBtn;
    [SerializeField] private Button equipItemBtn;
    [SerializeField] private Button dropItemBtn;

    private UIPopupsManager popupsManager;
    private ItemEntity selectedItem;

    public override void OnInit()
    {
        base.OnInit();

        popupsManager = ServiceLocator.GetService<UIPopupsManager>();

        useItemBtn.onClick.AddListener(() =>
        {
            selectedItem.UseItem();
            popupsManager.CloseCurrentPopup();
        });

        equipItemBtn.onClick.AddListener(() =>
        {
            popupsManager.CloseCurrentPopup();
        });

        dropItemBtn.onClick.AddListener(() =>
        {
            popupsManager.CloseCurrentPopup();
        });
    }

    public void SelectItem(ItemEntity item)
    {
        selectedItem = item;

        useItemBtn.gameObject.SetActive(item.InfoPrefab.CanUse);
        equipItemBtn.gameObject.SetActive(item.InfoPrefab.CanEquip);
    }
}
