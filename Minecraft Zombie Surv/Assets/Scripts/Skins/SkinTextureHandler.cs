using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinTextureHandler : MonoBehaviour
{
    [SerializeField] private Renderer[] renders;

    private SkinsManager skinManager;

    private void Start()
    {
        try
        {
            skinManager = ServiceLocator.GetService<SkinsManager>();
            OnSkinChange(skinManager.SelectedTexture);

            skinManager.onSelectSkin += OnSkinChange;
        }
        catch
        {

        }
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
