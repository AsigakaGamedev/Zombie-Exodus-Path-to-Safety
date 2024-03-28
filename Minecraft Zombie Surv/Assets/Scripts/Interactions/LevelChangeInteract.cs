using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelChangeInteract : AInteractableComponent
{
    [Scene, SerializeField] private string nextScene;

    private UIPopupsManager popupsManager;
    private LocalizationManager localizationManager;
    private LoadingManager loadingManager;
    private AdsService ads;

    [Inject]
    private void Construct(UIPopupsManager popupsManager, LocalizationManager localizationManager, LoadingManager loadingManager)
    {
        this.popupsManager = popupsManager;
        this.loadingManager = loadingManager;
        this.localizationManager = localizationManager;
    }

    protected override void Start()
    {
        base.Start();

        if (ServicesManager.Instance)
        {
            ads = ServicesManager.Instance.Ads;
        }
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        string hintText = "next_level_load_hint";

        try 
        {
            hintText = localizationManager.CurrentLocalization.GetValue("next_level_load_hint");
        }
        catch { }

        popupsManager.OpenPopup<UIChoosesPopup>("chooses_panel")
            .ShowPopup(hintText,
            new UnityAction[]
            {
                () =>
                {
                    popupsManager.CloseCurrentPopup();
                },
                async () =>
                {
                    await loadingManager.LoadSceneAsync(nextScene);

                    try
                    {
                        ads.LoadInterstitial();
                    }
                    catch{}
                }
            });
    }
}
