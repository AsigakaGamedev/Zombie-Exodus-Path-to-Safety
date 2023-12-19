using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICraftManager : MonoBehaviour
{
    [SerializeField] private UICraftRecipe recipePrefab;

    [Space]
    [SerializeField] private Transform recipiesContent;

    private ObjectPoolingManager poolingManager;
    private LevelContoller levelContoller;
    private PlayerController player;

    private List<UICraftRecipe> spawnedRecipies;

    private void Start()
    {
        spawnedRecipies = new List<UICraftRecipe>();

        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();
        levelContoller = ServiceLocator.GetService<LevelContoller>();
    }

    private void OnEnable()
    {
        if (!levelContoller) return;

        if (!player && levelContoller)
        {
            player = levelContoller.PlayerInstance;

            if (!player) return;
        }

        foreach (CraftInfo recipe in player.Inventory.AllCrafts)
        {
            UICraftRecipe newRecipe = poolingManager.GetPoolable(recipePrefab);
            newRecipe.Init(recipe);
            newRecipe.transform.SetParent(recipiesContent);

            newRecipe.onClickInfo += OnSelectRecipe;

            spawnedRecipies.Add(newRecipe);
        }
    }

    private void OnDisable()
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
        print(craftInfo);
    }
}
