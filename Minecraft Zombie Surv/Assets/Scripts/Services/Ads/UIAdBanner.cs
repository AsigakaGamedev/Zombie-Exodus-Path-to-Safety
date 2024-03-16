using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAdBanner : MonoBehaviour
{
    [SerializeField] private IronSourceBannerPosition bannerPosition;

    private AdsService adsService;
    private bool bannerLoaded;

    private IronSourceBannerSize bannerSize = IronSourceBannerSize.BANNER;

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
            adsService = ServiceLocator.GetService<ServicesManager>().Ads;
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
