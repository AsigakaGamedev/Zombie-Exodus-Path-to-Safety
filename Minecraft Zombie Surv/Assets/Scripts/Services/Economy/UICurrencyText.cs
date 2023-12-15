using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Economy.Model;
using UnityEngine;

public class UICurrencyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI linkedText;
    [SerializeField] private string currencyID;

    private GameEconomyService economy;

    private void Start()
    {
        economy = ServiceLocator.GetService<ServicesManager>().Economy;
        economy.onCurrencyUpdate += OnUpdateCurrency;
    }

    private void OnDestroy()
    {
        economy.onCurrencyUpdate -= OnUpdateCurrency;
    }

    private void OnUpdateCurrency(string id, CurrencyDefinition currency)
    {
        if (id != currencyID) return;

        linkedText.text = currency.Initial.ToString();
    }
}
