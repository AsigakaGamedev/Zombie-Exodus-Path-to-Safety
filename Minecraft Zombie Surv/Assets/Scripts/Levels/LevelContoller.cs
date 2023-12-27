using Cinemachine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelContoller : MonoBehaviour
{
    [SerializeField] private PlayerController playerPrefab; 
    [SerializeField] private Transform playerSpawnPos;

    [Space]
    [SerializeField] private CinemachineVirtualCamera levelCamera;

    [Space]
    [SerializeField] private PlayableDirector startTimeline;

    [Space]
    [ReadOnly, SerializeField] private PlayerController playerInstance;

    private TimelineController timelineController;

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

        timelineController = ServiceLocator.GetService<TimelineController>();

        if (startTimeline)
        {
            timelineController.SetTimeline(startTimeline);
            timelineController.Play();
        }
    }

    public void SpawnPlayer(Vector3 spawnPos)
    {
        if (playerInstance) Destroy(playerInstance.gameObject);

        playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity, playerSpawnPos);
        levelCamera.m_Follow = playerInstance.transform;
        playerInstance.Init();
    }
}
