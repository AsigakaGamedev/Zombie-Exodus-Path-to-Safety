using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private LevelsManager levelsManager;
    [SerializeField] private SavesManager savesManager;
    [SerializeField] private SkinsManager skinsManager;
    [SerializeField] private LocalizationManager localizationManager;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private ServicesManager servicesManager;
    [SerializeField] private LoadingManager loadingManager;

    public override void InstallBindings()
    {
        Container.Bind<LevelsManager>().FromInstance(levelsManager).AsSingle();
        Container.Bind<PlayerManager>().FromInstance(playerManager).AsSingle();
        Container.Bind<SavesManager>().FromInstance(savesManager).AsSingle();
        Container.Bind<SkinsManager>().FromInstance(skinsManager).AsSingle();
        Container.Bind<LocalizationManager>().FromInstance(localizationManager).AsSingle();
        Container.Bind<SettingsManager>().FromInstance(settingsManager).AsSingle();
        Container.Bind<ServicesManager>().FromInstance(servicesManager).AsSingle();
        Container.Bind<LoadingManager>().FromInstance(loadingManager).AsSingle();

        print("Global Installer Binded");
    }
}
