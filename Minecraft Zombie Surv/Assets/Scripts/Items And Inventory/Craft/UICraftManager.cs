using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.CloudSave.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UICraftManager : MonoBehaviour
{
    [SerializeField] private UICraftRecipe uiCraftPrefab;
    [SerializeField] private Transform recipiesContent;
    [SerializeField] private UICraftTypeChanger[] typeChangers;

    [Header("Selected Craft")]
    [SerializeField] private GameObject selectedCraftPanel;
    [SerializeField] private Image selectedCraftImg;
    [SerializeField] private TextMeshProUGUI selectedCraftName;
    [SerializeField] private TextMeshProUGUI selectedCraftDesc;
    [SerializeField] private Button craftBtn;

    private ObjectPoolingManager poolingManager;
    private InventoryController playerInventory;
    private CraftsManager craftsManager;
    private LocalizationManager localizationManager;

    private CraftInfo selectedCraft;

    private List<UICraftRecipe> spawnedRecipies;

    private void Start()
    {
        spawnedRecipies = new List<UICraftRecipe>();

        playerInventory = ServiceLocator.GetService<PlayerController>().Inventory;
        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();
        craftsManager = ServiceLocator.GetService<CraftsManager>();
        localizationManager = ServiceLocator.GetService<LocalizationManager>();

        craftBtn.onClick.AddListener(() =>
        {
            playerInventory.CraftItem(selectedCraft);
            ShowSelectedCraft(selectedCraft);
        });

        foreach (var typeChanger in typeChangers)
        {
            typeChanger.onClickInfo += ShowByCraftType;
        }

        ShowByCraftType(CraftType.Weapon);
        selectedCraftPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (var typeChanger in typeChangers)
        {
            typeChanger.onClickInfo -= ShowByCraftType;
        }
    }

    private void OnDisable()
    {
        selectedCraftPanel.SetActive(false);
    }

    public void ShowSelectedCraft(CraftInfo craftInfo)
    {
        selectedCraft = craftInfo;
        selectedCraftPanel.SetActive(true);

        selectedCraftImg.sprite = craftInfo.CraftSprite;
        selectedCraftName.text = localizationManager.CurrentLocalization.GetValue(craftInfo.CraftNameKey);
        selectedCraftDesc.text = localizationManager.CurrentLocalization.GetValue(craftInfo.CraftDescKey);

        craftBtn.interactable = playerInventory.CanCraftItem(selectedCraft);
    }

    public void ShowByCraftType(CraftType craftType)
    {
        foreach (UICraftRecipe spawnedCraft in spawnedRecipies)
        {
            spawnedCraft.onClickInfo -= ShowSelectedCraft;
            spawnedCraft.gameObject.SetActive(false);
        }

        spawnedRecipies.Clear();

        foreach (CraftInfo craftInfo in craftsManager.GetCrafts(craftType))
        {
            UICraftRecipe newUICraft = poolingManager.GetPoolable(uiCraftPrefab);
            newUICraft.transform.SetParent(recipiesContent);
            newUICraft.transform.localScale = Vector3.one;
            newUICraft.Init(craftInfo);
            newUICraft.onClickInfo += ShowSelectedCraft;
            spawnedRecipies.Add(newUICraft);
        }
    }
}
