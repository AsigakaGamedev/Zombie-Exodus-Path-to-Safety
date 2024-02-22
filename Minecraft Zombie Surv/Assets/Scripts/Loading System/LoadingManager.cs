using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : AInitializable
{
    [SerializeField] private GameObject screenObject;

    [Space]
    [SerializeField] private Slider loadingProgressBar;
    [SerializeField] private TextMeshProUGUI loadingHintTxt;
    [SerializeField] private Image loadingPreviewImg;

    public Action onLoadingStart;
    public Action onLoadingFinish;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public override void OnInit()
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
        onLoadingStart?.Invoke();

        AsyncOperation loadingOp = SceneManager.LoadSceneAsync(sceneName);
        
        while (!loadingOp.isDone)
        {
            loadingProgressBar.value = loadingOp.progress;
            yield return null;
        }

        screenObject.SetActive(false);
        onLoadingFinish?.Invoke();
    }

    public async Task LoadSceneAsync(string sceneName)
    {
        await LoadSceneAsyncTask(sceneName, null);
    }

    public async Task LoadSceneAsync(string sceneName, Task[] tasks = null)
    {
        await LoadSceneAsyncTask(sceneName, tasks);
    }

    private async Task LoadSceneAsyncTask(string sceneName, Task[] tasks)
    {
        screenObject.SetActive(true);
        onLoadingStart?.Invoke();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        loadingHintTxt.text = "Загрузка сцены";

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            //Debug.Log("Прогресс загрузки: " + (progress * 100) + "%");
            loadingProgressBar.value = progress;

            await Task.Yield();
        }

        if (tasks != null)
        {
            loadingProgressBar.value = 0;
            loadingHintTxt.text = "Загрузка сервисов";
            float progressStep = 1f / tasks.Length;
            float tasksProgress = 0;

            foreach (Task task in tasks)
            {
                await task;
                tasksProgress += progressStep;
                await Task.Yield();
                loadingProgressBar.value = tasksProgress;
                await Task.Yield();
            }
        }

        Debug.Log("Сцена загружена!");
        screenObject.SetActive(false);
        onLoadingFinish?.Invoke();
    }
}
