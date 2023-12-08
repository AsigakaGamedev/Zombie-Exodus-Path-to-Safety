using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;

    [Space]
    [SerializeField] private Texture2D[] textures;

    private void Start()
    {
        SetRandomTexture();
    }

    [Button]
    public void InitRenders()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    [Button]
    public void SetRandomTexture()
    {
        Texture2D texture = textures[Random.Range(0, textures.Length)];

        foreach (var renderer in renderers)
        {
            renderer.material.mainTexture = texture;
        }
    }
}
