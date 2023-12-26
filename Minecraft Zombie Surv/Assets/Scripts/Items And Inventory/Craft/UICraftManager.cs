using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public Action<CraftInfo> onCraft;

    [Space]
    [SerializeField] private Transform craftPriceSlot;
    [SerializeField] private Transform craftTypePanel;

    [SerializeField] private CraftType craftType;

    [SerializeField] private InventoryController inventoryController;

    private void Awake()
    {
        craftBtn.onClick.AddListener(() =>
        {
            onCraft?.Invoke(curCraftInfo);
        });
    }

    private void Start()
    {
        onCraft += inventoryController.CraftItem;
        spawnedRecipies = new List<UICraftRecipe>();

        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();
        levelContoller = ServiceLocator.GetService<LevelContoller>();
        DeactivateChildObjects();
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
        if (!levelContoller) return;

        if (!player && levelContoller)
        {
            player = levelContoller.PlayerInstance;

            if (!player) return;
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
    }

    private void SpawnSlots(CraftType _craftType = CraftType.Block)
    {
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
        curCraftInfo = craftInfo;
        itemName.text = craftInfo.CraftName;
        itemDescription.text = craftInfo.CraftDescription;

        DeactivateChildObjects();
        for (int i = 0; i < craftInfo.CreationPriceList.Count; i++)
        {
            if (i < craftPriceSlot.transform.childCount)
            {
                craftPriceSlot.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
