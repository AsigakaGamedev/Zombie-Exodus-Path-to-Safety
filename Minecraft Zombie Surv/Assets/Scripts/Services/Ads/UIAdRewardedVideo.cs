using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAdRewardedVideo : MonoBehaviour
{
    [SerializeField] private Button linkedBtn;

    [Space]
    [SerializeField] private string currencyID = "MONEY";
    [SerializeField] private int currencyIncrement = 100;

    private AdsService adsService;

    private void Start()
    {
        adsService = ServiceLocator.GetService<ServicesManager>().Ads;

        linkedBtn.onClick.AddListener(() =>
        {
            StartCoroutine(adsService.LoadAndShowRewarded(currencyID, currencyIncrement));
        });
    }
}
