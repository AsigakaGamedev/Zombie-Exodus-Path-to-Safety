using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private string playerNickname;

    private ServicesManager servicesManager;
    private GameCloudService cloudService;

    public Action<string> onNicknameChange;

    public string PlayerNickname { get => playerNickname; }

    [Inject]
    private void Construct(ServicesManager servicesManager)
    {
        this.servicesManager = servicesManager;
    }

    private void Start()
    {
        cloudService = servicesManager.Cloud;
        cloudService.onLoadPlayerData += OnLoadPlayerData;
    }

    private void OnDestroy()
    {
        cloudService.onLoadPlayerData -= OnLoadPlayerData;
    }

    private void OnLoadPlayerData(PlayerCloudData playerData)
    {
        playerNickname = playerData.Nickname;
        onNicknameChange?.Invoke(playerNickname);
    }

    public void SetNickname(string newNick)
    {
        if (newNick == playerNickname) return;

        playerNickname = newNick;
        onNicknameChange?.Invoke(playerNickname);
    }
}
