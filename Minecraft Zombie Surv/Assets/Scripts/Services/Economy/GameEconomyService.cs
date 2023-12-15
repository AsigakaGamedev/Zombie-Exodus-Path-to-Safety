using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

public class GameEconomyService : MonoBehaviour
{
    private Dictionary<string, CurrencyDefinition> currencies;

    public Action<string, CurrencyDefinition> onCurrencyUpdate;

    public async void Refresh()
    {
        currencies = new Dictionary<string, CurrencyDefinition>();

        //List<ConfigurationItemDefinition> configurationItemDefinitions = await EconomyService.Instance.Configuration.SyncConfigurationAsync();
        List<CurrencyDefinition> rawCurrencies = await EconomyService.Instance.Configuration.GetCurrenciesAsync();

        foreach (CurrencyDefinition curCurrency in rawCurrencies)
        {
            currencies.Add(curCurrency.Id, curCurrency);
            onCurrencyUpdate?.Invoke(curCurrency.Id, curCurrency);
            print($"Currency {curCurrency.Id} was added!");
        }

        print($"Economy updated\nCurrencies count - {currencies.Count}");
    }

    public bool TryGetCurrency(string id, out CurrencyDefinition currency)
    {
        return currencies.TryGetValue(id, out currency);
    }
}
