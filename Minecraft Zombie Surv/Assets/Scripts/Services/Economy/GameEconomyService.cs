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

    public Action<PlayerBalance> onPlayerBalanceUpdate;

    public async Task Refresh()
    {
        currencies = new Dictionary<string, CurrencyDefinition>();

        //List<ConfigurationItemDefinition> configurationItemDefinitions = await EconomyService.Instance.Configuration.SyncConfigurationAsync();
        List<CurrencyDefinition> rawCurrencies = await EconomyService.Instance.Configuration.GetCurrenciesAsync();

        foreach (CurrencyDefinition curCurrency in rawCurrencies)
        {
            currencies.Add(curCurrency.Id, curCurrency);
            //print($"Currency {curCurrency.Id} was added!");
        }

        GetBalancesResult results = await EconomyService.Instance.PlayerBalances.GetBalancesAsync();

        foreach (PlayerBalance balance in results.Balances)
        {
            //print($"Balance {balance.CurrencyId} is {balance.Balance}");
            onPlayerBalanceUpdate?.Invoke(balance);
        }
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
        PlayerBalance playerBalance = await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(id, amount);
        onPlayerBalanceUpdate?.Invoke(playerBalance);
    }
}
