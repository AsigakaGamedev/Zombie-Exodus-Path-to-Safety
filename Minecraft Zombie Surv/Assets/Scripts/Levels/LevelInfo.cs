using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Level")]
public class LevelInfo : ScriptableObject
{
    [Scene, SerializeField] private string sceneName;

    [Space]
    [SerializeField] private Sprite levelPreview;
    [SerializeField] private string levelName;
    [SerializeField] private string levelDescription;

    public string SceneName { get => sceneName; }
    public Sprite LevelPreview { get => levelPreview; }
    public string LevelName { get => levelName; }
    public string LevelDescription { get => levelDescription; }

}
