using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Economy.Model;
using UnityEngine;
using UnityEngine.UI;

public class UISelectedItemPopup : APopup
{
    [SerializeField] private Image itemIcon; 
    [SerializeField] private TextMeshProUGUI itemName; 
    [SerializeField] private TextMeshProUGUI itemDesc; 

    [Space]
    [SerializeField] private Button useItemBtn;
    [SerializeField] private Button equipItemBtn;
    [SerializeField] private Button dropItemBtn;

    private UIPopupsManager popupsManager;
    private LocalizationManager localizationManager;
    private ItemEntity selectedItem;

    public override void OnInit()
    {
        base.OnInit();

        popupsManager = ServiceLocator.GetService<UIPopupsManager>();
        localizationManager = ServiceLocator.GetService<LocalizationManager>();

        useItemBtn.onClick.AddListener(() =>
        {
            selectedItem.UseItem();
            popupsManager.CloseCurrentPopup();
        });

        equipItemBtn.onClick.AddListener(() =>
        {
            selectedItem.EquipItem();
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

        ItemInfo info = item.InfoPrefab;

        itemIcon.sprite = info.ItemCellSprite;

        itemName.text = localizationManager.CurrentLocalization.GetValue(info.ItemNameKey);
        itemDesc.text = localizationManager.CurrentLocalization.GetValue(info.ItemDescriptionKey);

        useItemBtn.gameObject.SetActive(info.CanUse);
        equipItemBtn.gameObject.SetActive(info.CanEquip);
    }
}
