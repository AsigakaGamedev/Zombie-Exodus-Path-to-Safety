using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private string playerNickname;

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

    public void SetNickname(string newNick)
    {
        if (newNick == playerNickname) return;

        playerNickname = newNick;
        onNicknameChange?.Invoke(playerNickname);
    }
}
