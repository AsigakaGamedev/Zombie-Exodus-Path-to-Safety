using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UINewGameBtn : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [Scene, SerializeField] private string firstLevelScene;

    private LoadingManager loadingManager;
    private UIPopupsManager popupsManager;
    private LocalizationManager localizationManager;

    private void Start()
    {
        loadingManager = ServiceLocator.GetService<LoadingManager>();
        popupsManager = ServiceLocator.GetService<UIPopupsManager>();
        localizationManager = ServiceLocator.GetService<LocalizationManager>();

        newGameBtn.onClick.AddListener(async () =>
        {
            if (true) //Если есть сохранения
            {
                popupsManager.OpenPopup<UIChoosesPopup>("yes_or_no").
                ShowPopup(localizationManager.CurrentLocalization.GetValue("start_new_game_hint"),
                new UnityAction[] 
                { 
                    () =>
                    {
                        popupsManager.CloseCurrentPopup();
                    },
                    async () =>
                    {
                        newGameBtn.interactable = false;
                        await loadingManager.LoadSceneAsync(firstLevelScene);
                    }
                });
            }
            else
            {
                newGameBtn.interactable = false;
                await loadingManager.LoadSceneAsync(firstLevelScene);
            }
        });
    }
}
