using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private LevelInfo[] allMaps;

    [Space]
    [SerializeField] private LevelInfo selectedLevel;

    private LoadingManager loadingManager;

    public LevelInfo[] AllLevels { get => allMaps; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        loadingManager = ServiceLocator.GetService<LoadingManager>();
    }

    [Button]
    public void LoadSelectedLevel()
    {
        loadingManager.LoadScene(selectedLevel.SceneName);
    }

    public void SelectLevel(LevelInfo level)
    {
        selectedLevel = level;
    }
}
