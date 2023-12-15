using Cinemachine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    [Space]
    [ReadOnly, SerializeField] private CinemachineVirtualCamera curCamera;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void SetCamera(CinemachineVirtualCamera newCamera)
    {
        if (curCamera) curCamera.Priority = 0;

        curCamera = newCamera;
        curCamera.Priority = 10;
    }
}
