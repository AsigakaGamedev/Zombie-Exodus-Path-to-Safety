using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Economy.Model;
using UnityEngine;
using Zenject;

public class UICurrencyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI linkedText;
    [SerializeField] private string currencyID;

    private ServicesManager servicesManager;
    private GameEconomyService economy;

    [Inject]
    private void Construct(ServicesManager servicesManager)
    {
        this.servicesManager = servicesManager;
    }

    private async void Start()
    {
        while (!servicesManager.IsInitialized)
        {
            await Task.Delay(500);
        }

        economy = servicesManager.Economy;
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
