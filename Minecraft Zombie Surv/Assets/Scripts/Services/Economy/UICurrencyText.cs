using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Economy.Model;
using UnityEngine;

public class UICurrencyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI linkedText;
    [SerializeField] private string currencyID;

    private ServicesManager services;
    private GameEconomyService economy;

    private async void Start()
    {
        services = ServiceLocator.GetService<ServicesManager>();

        while (!services.IsInitialized)
        {
            await Task.Delay(500);
        }

        economy = services.Economy;
        economy.onPlayerBalanceUpdate += OnUpdateBalance;

        UpdateBalance();
    }

    private void OnDestroy()
    {
        if (economy)
            economy.onPlayerBalanceUpdate -= OnUpdateBalance;
    }

    private async void UpdateBalance()
    {
        PlayerBalance playerBalance = await economy.TryGetBalance(currencyID);
        if (playerBalance != null)
        {
            linkedText.text = playerBalance.Balance.ToString();
        }
    }

    private void OnUpdateBalance(PlayerBalance playerBalance)
    {
        if (playerBalance.CurrencyId != currencyID) return;

        linkedText.text = playerBalance.Balance.ToString();
    }
}
