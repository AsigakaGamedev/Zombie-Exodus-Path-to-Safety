using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject screenObject;

    [Space]
    [SerializeField] private Slider loadingProgressBar;
    [SerializeField] private TextMeshProUGUI loadingHintTxt;
    [SerializeField] private Image loadingPreviewImg;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        screenObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(ELoadScene(sceneName));
    }

    public IEnumerator ELoadScene(string sceneName)
    {
        screenObject.SetActive(true);

        AsyncOperation loadingOp = SceneManager.LoadSceneAsync(sceneName);

        while (!loadingOp.isDone)
        {
            loadingProgressBar.value = loadingOp.progress;
            yield return null;
        }

        screenObject.SetActive(false);
    }
}
