using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

public class GameEconomyService : MonoBehaviour
{
    private Dictionary<string, CurrencyDefinition> currencies;

    public Action<string, CurrencyDefinition> onCurrencyUpdate;

    public async Task Refresh()
    {
        currencies = new Dictionary<string, CurrencyDefinition>();

        //List<ConfigurationItemDefinition> configurationItemDefinitions = await EconomyService.Instance.Configuration.SyncConfigurationAsync();
        List<CurrencyDefinition> rawCurrencies = await EconomyService.Instance.Configuration.GetCurrenciesAsync();

        foreach (CurrencyDefinition curCurrency in rawCurrencies)
        {
            currencies.Add(curCurrency.Id, curCurrency);
            onCurrencyUpdate?.Invoke(curCurrency.Id, curCurrency);
            print($"Currency {curCurrency.Id} was added! {curCurrency.Modified}");
        }

        print($"Economy updated\nCurrencies count - {currencies.Count}");
    }

    public async Task<PlayerBalance> TryGetBalance(string currencyID)
    {
        GetBalancesResult results = await EconomyService.Instance.PlayerBalances.GetBalancesAsync();
        
        foreach (PlayerBalance balance in results.Balances)
        {
            if (balance.CurrencyId == currencyID)
            {
                return balance;
            }
        }

        return null;
    }

    public async Task IncrementBalance(string id, int amount)
    {
        await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(id, amount);
    }
}
