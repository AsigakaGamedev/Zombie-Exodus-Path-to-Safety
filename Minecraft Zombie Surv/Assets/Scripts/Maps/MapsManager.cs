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

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    [Button]
    public void LoadSelectedMap()
    {
        SceneManager.LoadScene(selectedMap.SceneName);
    }
}
