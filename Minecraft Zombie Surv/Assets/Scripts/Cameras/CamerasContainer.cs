using Cinemachine;
using System.Collections;
using UnityEngine;

public class CamerasContainer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] allCameras;

    private CamerasManager camerasManager;
    private int curCameraIndex;

    private void Start()
    {
        camerasManager = ServiceLocator.GetService<CamerasManager>();
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