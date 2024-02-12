using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasContainerInteract : AInteractableComponent
{
    [SerializeField] private CamerasContainer camerasContainer;

    private UICamerasContainerManager uiContainersManager;
    private UIManager uiManager;

    protected override void Start()
    {
        base.Start();

        uiManager = ServiceLocator.GetService<UIManager>();
        uiContainersManager = ServiceLocator.GetService<UICamerasContainerManager>();
    }

    protected override void OnInteract(PlayerController player)
    {
        uiContainersManager.OpenContainer(camerasContainer);
        uiManager.ChangeScreen("sequrity_cameras");
    }
}
