using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectedItemPopup : APopup
{
    [SerializeField] private Button useItemBtn;
    [SerializeField] private Button equipItemBtn;
    [SerializeField] private Button dropItemBtn;

    private UIPopupsManager popupsManager;

    public override void OnInit()
    {
        base.OnInit();

        popupsManager = ServiceLocator.GetService<UIPopupsManager>();

        useItemBtn.onClick.AddListener(() =>
        {
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
        print($"Item Selected {item.InfoPrefab}");
    }
}
