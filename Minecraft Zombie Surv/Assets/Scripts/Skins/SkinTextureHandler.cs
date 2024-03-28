using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkinTextureHandler : MonoBehaviour
{
    [SerializeField] private Renderer[] renders;

    private SkinsManager skinManager;

    [Inject]
    private void Construct(SkinsManager skinsManager)
    {
        OnSkinChange(skinManager.SelectedTexture);

        skinManager.onSelectSkin += OnSkinChange;
    }

    private void OnDestroy()
    {
        if (skinManager) skinManager.onSelectSkin -= OnSkinChange;
    }

    private void OnSkinChange(Texture2D texture)
    {
        foreach (var render in renders)
        {
            render.sharedMaterial.mainTexture = texture;
        }
    }
}
