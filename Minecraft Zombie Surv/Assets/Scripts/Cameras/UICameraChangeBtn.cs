using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UICameraChangeBtn : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera targetCamera;

    private Button btn;
    private CamerasManager camerasManager;

    [Inject]
    private void Construct(CamerasManager camerasManager)
    {
        this.camerasManager = camerasManager;
    }

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            camerasManager.SetCameraByPriority(targetCamera);
        });
    }
}
