using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelContoller : MonoBehaviour
{
    [SerializeField] private AInitializable[] levelManagers;

    private void Start()
    {
        foreach (var manager in levelManagers)
        {
            manager.OnInit();
        }
    }
}
