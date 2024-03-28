using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIAdBanner : MonoBehaviour
{
    [SerializeField] private IronSourceBannerPosition bannerPosition;

    private ServicesManager servicesManager;
    private AdsService adsService;
    private bool bannerLoaded;

    private IronSourceBannerSize bannerSize = IronSourceBannerSize.BANNER;

    [Inject]
    private void Construct(ServicesManager servicesManager)
    {
        this.servicesManager = servicesManager;
    }

    private void OnEnable()
    {
        if (bannerLoaded && adsService)
        {
            adsService.ShowBanner();
        }
    }

    private void OnDisable()
    {
        if (bannerLoaded && adsService)
        {
            adsService.HideBanner();
        }
    }

    private void Start()
    {
        try
        {
            adsService = servicesManager.Ads;
            adsService.LoadBanner(bannerSize, bannerPosition);
            bannerLoaded = true;
        }
        catch { }
    }

    private void OnDestroy()
    {
        try
        {
            adsService.DestroyBanner();
        }
        catch { }
    }
}
