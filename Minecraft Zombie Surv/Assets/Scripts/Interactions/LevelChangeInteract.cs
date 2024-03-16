using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelChangeInteract : AInteractableComponent
{
    [Scene, SerializeField] private string nextScene;

    private UIPopupsManager popupsManager;
    private LocalizationManager localizationManager;
    private LoadingManager loadingManager;
    private AdsService ads;

    protected override void Start()
    {
        base.Start();

        ads = ServiceLocator.GetService<ServicesManager>().Ads;

        popupsManager = ServiceLocator.GetService<UIPopupsManager>();
        localizationManager = ServiceLocator.GetServiceSafe<LocalizationManager>();
        loadingManager = ServiceLocator.GetServiceSafe<LoadingManager>();
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
                    if (loadingManager)
                    {
                        await loadingManager.LoadSceneAsync(nextScene);
                    }
                    else
                    {
                        SceneManager.LoadScene(nextScene);
                    }

                    try
                    {
                        ads.LoadInterstitial();
                    }
                    catch{}
                }
            });
    }
}
