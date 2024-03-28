using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private LevelInfo[] allMaps;

    [Space]
    [SerializeField] private LevelInfo selectedLevel;

    private LoadingManager loadingManager;

    public LevelInfo[] AllLevels { get => allMaps; }

    [Inject]
    private void Construct(LoadingManager loadingManager)
    {
        this.loadingManager = loadingManager;
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
