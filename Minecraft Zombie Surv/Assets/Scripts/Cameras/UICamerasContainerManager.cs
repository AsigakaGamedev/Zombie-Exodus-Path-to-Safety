using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICamerasContainerManager : MonoBehaviour
{
    [SerializeField] private Button nextCamBtn;
    [SerializeField] private Button prevCamBtn;

    private CamerasContainer container;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);

        if (container)
        {
            container.CloseContainer();
        }
    }

    private void Start()
    {
        nextCamBtn.onClick.AddListener(() =>
        {
            container.ShowNextCamera();
        });

        prevCamBtn.onClick.AddListener(() =>
        {
            container.ShowPrevCamera();
        });
    }

    public void OpenContainer(CamerasContainer container)
    {
        this.container = container;
        container.ShowCurCamera();
    }
}
