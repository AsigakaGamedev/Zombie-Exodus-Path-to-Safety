using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelChangeInteract : AInteractableComponent
{
    [Scene, SerializeField] private string nextScene;

    private UIPopupsManager popupsManager;
    private LocalizationManager localizationManager;
    private LoadingManager loadingManager;

    protected override void Start()
    {
        base.Start();

        popupsManager = ServiceLocator.GetService<UIPopupsManager>();
        localizationManager = ServiceLocator.GetService<LocalizationManager>();
        loadingManager = ServiceLocator.GetService<LoadingManager>();
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        popupsManager.OpenPopup<UIChoosesPopup>("chooses_panel")
            .ShowPopup(localizationManager.CurrentLocalization.GetValue("next_level_load_hint"),
            new UnityAction[]
            {
                () =>
                {
                    popupsManager.CloseCurrentPopup();
                },
                async () =>
                {
                    await loadingManager.LoadSceneAsync(nextScene);
                }
            });
    }
}
