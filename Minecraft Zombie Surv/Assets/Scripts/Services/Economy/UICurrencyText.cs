using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Economy.Model;
using UnityEngine;

public class UICurrencyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI linkedText;
    [SerializeField] private string currencyID;

    private ServicesManager services;
    private GameEconomyService economy;

    private void Start()
    {
        services = ServiceLocator.GetService<ServicesManager>();
        services.onInitialized += OnServicesInitialized;
    }

    private void OnDestroy()
    {
        services.onInitialized -= OnServicesInitialized;

        if (economy)
            economy.onCurrencyUpdate -= OnUpdateCurrency;
    }

    private async void OnServicesInitialized()
    {
        economy = ServiceLocator.GetService<ServicesManager>().Economy;
        economy.onCurrencyUpdate += OnUpdateCurrency;

        PlayerBalance playerBalance = await economy.TryGetBalance(currencyID);
        if (playerBalance != null)
        {
            linkedText.text = playerBalance.Balance.ToString();
        }
    }

    private void OnUpdateCurrency(string id, CurrencyDefinition currency)
    {
        if (id != currencyID) return;

        linkedText.text = currency.Initial.ToString();
    }
}
