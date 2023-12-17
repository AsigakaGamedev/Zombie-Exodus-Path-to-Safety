using Cinemachine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContoller : MonoBehaviour
{
    [SerializeField] private PlayerController playerPrefab; 
    [SerializeField] private Transform playerSpawnPos;

    [Space]
    [SerializeField] private CinemachineVirtualCamera levelCamera;

    [Space]
    [ReadOnly, SerializeField] private PlayerController playerInstance;

    public PlayerController PlayerInstance { get => playerInstance; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        SpawnPlayer(playerSpawnPos.position);
    }

    public void SpawnPlayer(Vector3 spawnPos)
    {
        if (playerInstance) Destroy(playerInstance.gameObject);

        playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        levelCamera.m_Follow = playerInstance.transform;
        playerInstance.Init();
    }
}
