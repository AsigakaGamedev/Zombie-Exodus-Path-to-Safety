using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;

public class GameCloudService : MonoBehaviour
{
    private PlayerManager playerManager;

    private const string PLAYER_DATA_KEY = "PLAYER_DATA";

    public Action<PlayerCloudData> onLoadPlayerData;

    public async Task Init()
    {
        await Task.Run(() =>
        {

        });
    }

    private void OnDestroy()
    {
        if (playerManager) playerManager.onNicknameChange -= OnPlayerChangeNickname;
    }

    private async void OnPlayerChangeNickname(string newNickname)
    {
        await SavePlayerData();
    }

    public async Task SavePlayerData()
    {
        if (!playerManager)
        {
            await Task.Run(() =>
            {
                playerManager = ServiceLocator.GetService<PlayerManager>();
                playerManager.onNicknameChange += OnPlayerChangeNickname;
            });
        }

        PlayerCloudData playerData = new PlayerCloudData()
        {
            Nickname = playerManager.PlayerNickname
        };

        Dictionary<string, object> data = new Dictionary<string, object>() { {PLAYER_DATA_KEY, playerData } };

        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

        print($"Player data saved! {playerData.ToString()}");
    }

    public async Task LoadPlayerData()
    {
        Dictionary<string, Item> data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { PLAYER_DATA_KEY});

        try
        {
            PlayerCloudData playerData = JsonUtility.FromJson<PlayerCloudData>(data[PLAYER_DATA_KEY].Value.GetAsString());

            print($"Player data loaded! {playerData}");
            onLoadPlayerData?.Invoke(playerData);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Player data not saved");
        }
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
