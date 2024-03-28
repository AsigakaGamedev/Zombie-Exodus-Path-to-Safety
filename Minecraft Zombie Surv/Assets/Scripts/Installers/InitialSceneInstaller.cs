using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InitialSceneInstaller : MonoInstaller
{
    [SerializeField] private AudioManager audioManager;

    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromInstance(audioManager);
    }
}
