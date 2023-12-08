using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Map")]
public class MapInfo : ScriptableObject
{
    [Scene, SerializeField] private string sceneName;

    public string SceneName { get => sceneName; }
}
