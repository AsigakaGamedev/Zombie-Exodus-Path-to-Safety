using Cinemachine;
using System.Collections;
using UnityEngine;
using Zenject;

public class CamerasContainer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] allCameras;

    private CamerasManager camerasManager;
    private int curCameraIndex;

    [Inject]
    private void Construct(CamerasManager camerasManager)
    {
        this.camerasManager = camerasManager;
    }

    private void Awake()
    {
        curCameraIndex = 0;
    }

    public void CloseContainer()
    {
        camerasManager.SetDefaultCamera();
    }

    public void ShowCurCamera()
    {
        ShowCameraByIndex(curCameraIndex);
    }

    public void ShowNextCamera()
    {
        ShowCameraByIndex(curCameraIndex + 1);
    }

    public void ShowPrevCamera()
    {
        ShowCameraByIndex(curCameraIndex - 1);
    }

    public void ShowCameraByIndex(int index)
    {
        if (index < 0) index = allCameras.Length;
        else if (index >= allCameras.Length) index = 0;

        curCameraIndex = index;
        camerasManager.SetCameraByActive(allCameras[curCameraIndex]);
    }
}