using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Economy.Model;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISelectedItemPopup : APopup
{
    [SerializeField] private Image itemIcon; 
    [SerializeField] private TextMeshProUGUI itemName; 
    [SerializeField] private TextMeshProUGUI itemDesc;

    [Space]
    [SerializeField] private SerializedDictionary<string, Color> needsColors;
    [SerializeField] private Color healthColor;

    [Space]
    [SerializeField] private Button useItemBtn;
    [SerializeField] private Button equipItemBtn;
    [SerializeField] private Button dropItemBtn;

    private UIPopupsManager popupsManager;
    private LocalizationManager localizationManager;
    private ItemEntity selectedItem;

    [Inject]
    private void Construct(UIPopupsManager popupsManager, LocalizationManager localizationManager)
    {
        this.popupsManager = popupsManager;
        this.localizationManager = localizationManager;
    }

    public override void OnInit()
    {
        base.OnInit();

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

        useItemBtn.gameObject.SetActive(info.CanUse);
        equipItemBtn.gameObject.SetActive(info.CanEquip || info.IsWeapon);

        if (!localizationManager) return;

        itemName.text = localizationManager.CurrentLocalization.GetValue(info.ItemNameKey);

        string resultDescription = localizationManager.CurrentLocalization.GetValue(info.ItemDescriptionKey);
        
        if (info.CanUse)
        {
            foreach (ItemUseData useData in info.UseDatas)
            {
                string hexColor = ColorUtility.ToHtmlStringRGB(needsColors[useData.NeedID]);

                string valueOperator = useData.NeedIncreaseValue > 0 ? "+" : "";

                string valueName = localizationManager.CurrentLocalization.GetValue(useData.NeedID);

                resultDescription += $"\n<color=#{hexColor}>{valueOperator}{useData.NeedIncreaseValue}  {valueName}</color>";
            }

            if (info.HealthIncrease != 0)
            {
                string hexColor = ColorUtility.ToHtmlStringRGB(healthColor);

                string valueOperator = info.HealthIncrease > 0 ? "+" : "";

                string valueName = localizationManager.CurrentLocalization.GetValue("health");

                resultDescription += $"\n<color=#{hexColor}>{valueOperator}{info.HealthIncrease}  {valueName}</color>";
            }
        }

        itemDesc.text = resultDescription;
    }
}
