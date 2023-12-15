using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UICameraChangeBtn : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera targetCamera;

    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            ServiceLocator.GetService<CamerasManager>().SetCamera(targetCamera);
        });
    }
}
