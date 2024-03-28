using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIAdRewardedVideo : MonoBehaviour
{
    [SerializeField] private Button linkedBtn;

    [Space]
    [SerializeField] private string currencyID = "MONEY";
    [SerializeField] private int currencyIncrement = 100;

    private ServicesManager servicesManager;
    private AdsService adsService;

    [Inject]
    private void Construct(ServicesManager servicesManager)
    {
        this.servicesManager = servicesManager;
    }

    private void Start()
    {
        adsService = servicesManager.Ads;

        linkedBtn.onClick.AddListener(() =>
        {
            StartCoroutine(adsService.LoadAndShowRewarded(currencyID, currencyIncrement));
        });
    }
}
