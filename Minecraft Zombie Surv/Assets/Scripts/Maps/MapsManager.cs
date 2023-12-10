using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapsManager : MonoBehaviour
{
    [SerializeField] private MapInfo[] allMaps;

    [Space]
    [SerializeField] private MapInfo selectedMap;

    private LoadingManager loadingManager;

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
    public void LoadSelectedMap()
    {
        loadingManager.LoadScene(selectedMap.SceneName);
    }
}
