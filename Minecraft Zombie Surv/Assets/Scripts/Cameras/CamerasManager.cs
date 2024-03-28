using Cinemachine;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera defaultCamera;

    [Space]
    [ReadOnly, SerializeField] private CinemachineVirtualCamera curCamera;

    private bool canShake;

    private void Start() 
    {
        canShake = true;
        curCamera = defaultCamera;
    }

    public void SetCameraByPriority(CinemachineVirtualCamera newCamera)
    {
        if (curCamera) curCamera.Priority = 0;

        curCamera = newCamera;
        curCamera.Priority = 10;
    }

    public void SetCameraByActive(CinemachineVirtualCamera newCamera)
    {
        if (curCamera) curCamera.gameObject.SetActive(false);

        curCamera = newCamera;
        curCamera.gameObject.SetActive(true);
    }

    public void SetDefaultCamera()
    {
        SetCameraByActive(defaultCamera);
    }

    public void Shake(float duration, float strenght)
    {
        if (!canShake) return;

        curCamera.transform.DOShakeScale(duration, strenght);
        canShake = false;
        Invoke(nameof(ResetCanShake), duration);
    }

    private void ResetCanShake()
    {
        canShake = true;
    }
}
