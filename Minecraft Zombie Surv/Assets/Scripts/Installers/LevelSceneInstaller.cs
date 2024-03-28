using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] private ObjectPoolingManager poolingManager;
    [SerializeField] private MonologsManager monologsManager;
    [SerializeField] private TimelineController timelineController;
    [SerializeField] private CamerasManager camerasManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CraftsManager craftsManager;

    [Header("UI")]
    [SerializeField] private UIMobilePlayerInputs uiMobileInputs;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UITerminalManager uiTerminal;
    [SerializeField] private UINoteManager uiNoteManager;
    [SerializeField] private UIPopupsManager uiPopupsManager;
    [SerializeField] private UIInventoriesManager uiInventoriesManager;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolingManager>().FromInstance(poolingManager).AsSingle();
        Container.Bind<MonologsManager>().FromInstance(monologsManager).AsSingle();
        Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
        Container.Bind<CamerasManager>().FromInstance(camerasManager).AsSingle();
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle();
        Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
        Container.Bind<CraftsManager>().FromInstance(craftsManager).AsSingle();

        Container.Bind<UIMobilePlayerInputs>().FromInstance(uiMobileInputs).AsSingle();
        Container.Bind<UIManager>().FromInstance(uiManager).AsSingle();
        Container.Bind<UITerminalManager>().FromInstance(uiTerminal).AsSingle();
        Container.Bind<UINoteManager>().FromInstance(uiNoteManager).AsSingle();
        Container.Bind<UIPopupsManager>().FromInstance(uiPopupsManager).AsSingle();
        Container.Bind<UIInventoriesManager>().FromInstance(uiInventoriesManager).AsSingle();

        print("Level Installer Binded");
    }
}
