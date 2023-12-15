using System.Collections;
using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;

public class GameCloudService : MonoBehaviour
{
    private PlayerManager playerManager;

    private const string PLAYER_DATA = "PLAYER_DATA";

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
        PlayerSaveData playerData = new PlayerSaveData()
        {
            Nickname = playerManager.PlayerNickname
        };

        Dictionary<string, object> data = new Dictionary<string, object>() { {PLAYER_DATA, playerData } };

        await CloudSaveService.Instance.Data.Player.SaveAsync(data);

        print("Player data saved!");
    }
}

public class PlayerSaveData
{
    public string Nickname;
}
