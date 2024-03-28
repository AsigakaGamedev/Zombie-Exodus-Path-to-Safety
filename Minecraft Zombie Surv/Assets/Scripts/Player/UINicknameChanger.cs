using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UINicknameChanger : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button changeNickBtn;

    private PlayerManager playerManager;

    [Inject]
    private void Construct(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    private void Start()
    {
        playerManager.onNicknameChange += OnNicknameChange;

        inputField.text = playerManager.PlayerNickname;

        changeNickBtn.onClick.AddListener(() =>
        {
            playerManager.SetNickname(inputField.text);
        });
    }

    private void OnDestroy()
    {
        playerManager.onNicknameChange -= OnNicknameChange;
    }

    private void OnNicknameChange(string newNick)
    {
        inputField.text = newNick;
    }
}
