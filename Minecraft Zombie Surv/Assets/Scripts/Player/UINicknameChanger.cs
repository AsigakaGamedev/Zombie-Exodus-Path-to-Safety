using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINicknameChanger : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button changeNickBtn;

    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = ServiceLocator.GetService<PlayerManager>();
        playerManager.onNicknameChange += OnNicknameChange;

        inputField.text = playerManager.PlayerNickname;

        changeNickBtn.onClick.AddListener(() =>
        {
            playerManager.SetNickname(inputField.text);
        });
    }

    private void OnDestroy()
    {
        if (playerManager) playerManager.onNicknameChange -= OnNicknameChange;
    }

    private void OnNicknameChange(string newNick)
    {
        inputField.text = newNick;
    }
}
