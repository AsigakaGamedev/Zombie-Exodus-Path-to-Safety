using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UILoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingProgressBar;
    [SerializeField] private TextMeshProUGUI loadingHintTxt;
    [SerializeField] private Image loadingPreviewImg;

    private LoadingManager loadingManager;

    [Inject]
    private void Construct(LoadingManager loadingManager)
    {
        this.loadingManager = loadingManager;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        loadingProgressBar.minValue = 0;
        loadingProgressBar.maxValue = 100;

        loadingManager.onLoadingStart += OnLoadingStart;
        loadingManager.onLoadingFinish += OnLoadingFinish;
        loadingManager.onLoadingProgressUpd += OnLoadingProgressUpd;
        loadingManager.onLoadingTextUpd += OnLoadingTextUpd;
    }

    private void OnDestroy()
    {
        loadingManager.onLoadingStart -= OnLoadingStart;
        loadingManager.onLoadingFinish -= OnLoadingFinish;
        loadingManager.onLoadingProgressUpd -= OnLoadingProgressUpd;
        loadingManager.onLoadingTextUpd -= OnLoadingTextUpd;
    }

    private void OnLoadingStart()
    {
        gameObject.SetActive(true);
    }

    private void OnLoadingFinish()
    {
        gameObject.SetActive(false);
    }

    private void OnLoadingProgressUpd(float progress)
    {
        loadingProgressBar.value = progress;
    }

    private void OnLoadingTextUpd(string text)
    {
        loadingHintTxt.text = text;
    }
}
