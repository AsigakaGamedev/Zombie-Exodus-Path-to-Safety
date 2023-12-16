using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private string playerNickname;

    private GameCloudService cloudService;

    public Action<string> onNicknameChange;

    public string PlayerNickname { get => playerNickname; }

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
        cloudService = ServiceLocator.GetService<ServicesManager>().Cloud;
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
