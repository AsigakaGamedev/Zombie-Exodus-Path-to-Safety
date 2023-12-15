using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;

public class GameCloudService : MonoBehaviour
{
    private PlayerManager playerManager;

    private const string PLAYER_DATA_KEY = "PLAYER_DATA";

    public Action<PlayerCloudData> onLoadPlayerData;

    public async void Init()
    {
        playerManager = ServiceLocator.GetService<PlayerManager>();
        playerManager.onNicknameChange += OnPlayerChangeNickname;
    }

    private void OnDestroy()
    {
        playerManager.onNicknameChange -= OnPlayerChangeNickname;
    }

    private void OnPlayerChangeNickname(string newNickname)
    {
        SavePlayerData();
    }

    public async void SavePlayerData()
    {
        PlayerCloudData playerData = new PlayerCloudData()
        {
            Nickname = playerManager.PlayerNickname
        };

        Dictionary<string, object> data = new Dictionary<string, object>() { {PLAYER_DATA_KEY, playerData } };

        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

        print($"Player data saved! {playerData.ToString()}");
    }

    public async void LoadPlayerData()
    {
        Dictionary<string, Item> data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { PLAYER_DATA_KEY});

        PlayerCloudData playerData = JsonUtility.FromJson<PlayerCloudData>(data[PLAYER_DATA_KEY].Value.GetAsString());

        print($"Player data loaded! {playerData.ToString()}");
        onLoadPlayerData?.Invoke(playerData);  
    }
}

public class PlayerCloudData
{
    public string Nickname;

    public override string ToString()
    {
        return $"Nickname {Nickname}";
    }
}
