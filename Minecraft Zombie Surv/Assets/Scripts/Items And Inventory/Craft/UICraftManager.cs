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
    [SerializeField] private UICraftRecipe recipePrefab;

    [Space]
    [SerializeField] private Transform recipiesContent;

    private ObjectPoolingManager poolingManager;
    private LevelContoller levelContoller;
    private PlayerController player;
    private CraftInfo curCraftInfo;

    private List<UICraftRecipe> spawnedRecipies;

    [Space]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Button craftBtn;
    [SerializeField] private Sprite enoughtMaterialSprite;
    [SerializeField] private Sprite notEnoughtMaterialSprite;

    public Action<CraftInfo> onCraft;

    [Space]
    [SerializeField] private Transform craftPriceSlot;
    [SerializeField] private Transform craftTypePanel;

    [SerializeField] private CraftType craftType;


    private void Awake()
    {
        craftBtn.onClick.AddListener(() =>
        {
            onCraft?.Invoke(curCraftInfo);
        });
        spawnedRecipies = new List<UICraftRecipe>();

        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();
        DeactivateChildObjects();
    }

    private void Start()
    {
        levelContoller = ServiceLocator.GetService<LevelContoller>();
    }

    private void DeactivateChildObjects()
    {
        foreach (Transform child in craftPriceSlot)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (!player && levelContoller)
        {
            player = levelContoller.PlayerInstance;

            if (!player) return;
            onCraft += levelContoller.PlayerInstance.Inventory.CraftItem;
        }

        foreach (Transform child in craftTypePanel)
        {
            UICraftTypeChanger typeChanger = child.GetComponent<UICraftTypeChanger>();

            if (typeChanger != null)
            {
                typeChanger.onClickInfo = null;
                typeChanger.onClickInfo += SpawnSlots;
            }
        }

        SpawnSlots();
    }

    private void OnDisable()
    {
        DeleteSlots();

        if (player)
        {
            onCraft -= player.Inventory.CraftItem;
        }
    }

    private void SpawnSlots(CraftType _craftType = CraftType.Block)
    {
        if (!player) return;
        DeleteSlots();
        foreach (CraftInfo recipe in player.Inventory.AllCrafts)
        {
            if (recipe.CraftType == _craftType)
            {
                UICraftRecipe newRecipe = poolingManager.GetPoolable(recipePrefab);
                newRecipe.Init(recipe);
                newRecipe.transform.SetParent(recipiesContent);

                newRecipe.onClickInfo += OnSelectRecipe;

                spawnedRecipies.Add(newRecipe);
            }
        }
    }

    private void DeleteSlots()
    {
        foreach (UICraftRecipe recipe in spawnedRecipies)
        {
            recipe.onClickInfo -= OnSelectRecipe;

            recipe.gameObject.SetActive(false);
        }

        spawnedRecipies.Clear();
    }

    private void OnSelectRecipe(CraftInfo craftInfo)
    {
        if (!player) return;
        curCraftInfo = craftInfo;
        itemName.text = craftInfo.CraftName;
        itemDescription.text = craftInfo.CraftDescription;

        DeactivateChildObjects();
        for (int i = 0; i < craftInfo.CreationPriceList.Count; i++)
        {
            if (i < craftPriceSlot.transform.childCount)
            {
                GameObject childObject = craftPriceSlot.transform.GetChild(i).gameObject;
                childObject.SetActive(true);

                int craftPrice = craftInfo.CreationPriceList[i].RandomAmount;
                int amountInInventory = player.Inventory.GetMaterialCount(craftInfo.CreationPriceList[i].Info);

                TextMeshProUGUI textMeshPro1 = childObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI textMeshPro2 = childObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                Image imageComponent = childObject.transform.GetChild(2).GetComponent<Image>();

                if (textMeshPro1 != null && textMeshPro2 != null)
                {
                    textMeshPro1.text = craftInfo.CreationPriceList[i].Info.ItemNameKey;
                    textMeshPro2.text = $"{amountInInventory}/{craftPrice}";

                    imageComponent.gameObject.SetActive(true);
                    imageComponent.sprite = notEnoughtMaterialSprite;

                    if (amountInInventory >= craftPrice)
                    {
                        imageComponent.sprite = enoughtMaterialSprite;
                    }
                }
            }
        }
    }
}
