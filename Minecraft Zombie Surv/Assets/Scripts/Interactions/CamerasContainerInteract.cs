using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CamerasContainerInteract : AInteractableComponent
{
    [SerializeField] private CamerasContainer camerasContainer;

    private UICamerasContainerManager uiCamerasContainer;
    private UIManager uiManager;

    [Inject]
    private void Construct(UIManager uiManager, UICamerasContainerManager uiCamerasContainer)
    {
        this.uiCamerasContainer = uiCamerasContainer;
        this.uiManager = uiManager;
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        uiCamerasContainer.OpenContainer(camerasContainer);
        uiManager.ChangeScreen("sequrity_cameras");
    }
}
