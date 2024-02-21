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
        adsService = ServiceLocator.GetService<ServicesManager>().Ads;
        adsService.LoadBanner(bannerSize, bannerPosition);
        bannerLoaded = true;
    }

    private void OnDestroy()
    {
        adsService.DestroyBanner();
    }
}
