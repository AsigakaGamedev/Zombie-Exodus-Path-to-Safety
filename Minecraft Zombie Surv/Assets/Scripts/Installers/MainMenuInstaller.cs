using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private ObjectPoolingManager poolingManager;
    [SerializeField] private CamerasManager camerasManager;
    [SerializeField] private AudioManager audioManager;

    [Header("UI")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UIPopupsManager uiPopupsManager;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolingManager>().FromInstance(poolingManager).AsSingle();
        Container.Bind<CamerasManager>().FromInstance(camerasManager).AsSingle();
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle();

        Container.Bind<UIManager>().FromInstance(uiManager).AsSingle();
        Container.Bind<UIPopupsManager>().FromInstance(uiPopupsManager).AsSingle();

        print("Main Menu Installer Binded");
    }
}
