using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Block")]
public class BlockInfo : ScriptableObject
{
    [SerializeField] private string blockName;

    [Space]
    [SerializeField] private Vector2Int textureTopSideID;
    [SerializeField] private Vector2Int textureBottomSideID;
    [SerializeField] private Vector2Int textureHorizontalSideID;

    public string BlockName { get => blockName; }

    public Vector2Int TextureTopSideID { get => textureTopSideID; }
    public Vector2Int TextureBottomSideID { get => textureBottomSideID; }
    public Vector2Int TextureHorizontalSideID { get => textureHorizontalSideID; }
}
