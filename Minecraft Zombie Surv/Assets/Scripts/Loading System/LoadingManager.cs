using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Action onLoadingStart;
    public Action onLoadingFinish;
    public Action<float> onLoadingProgressUpd;
    public Action<string> onLoadingTextUpd;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(ELoadScene(sceneName));
    }

    public IEnumerator ELoadScene(string sceneName)
    {
        onLoadingStart?.Invoke();
        onLoadingTextUpd?.Invoke("Загрузка сцены");

        AsyncOperation loadingOp = SceneManager.LoadSceneAsync(sceneName);
        
        while (!loadingOp.isDone)
        {
            onLoadingProgressUpd?.Invoke(loadingOp.progress);
            yield return null;
        }

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
        onLoadingStart?.Invoke();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        onLoadingTextUpd?.Invoke("Загрузка сцены");

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            onLoadingProgressUpd?.Invoke(progress);

            await Task.Yield();
        }

        if (tasks != null)
        {
            onLoadingProgressUpd?.Invoke(0);
            onLoadingTextUpd?.Invoke("Загрузка сервисов");
            float progressStep = 1f / tasks.Length;
            float tasksProgress = 0;

            foreach (Task task in tasks)
            {
                await task;
                tasksProgress += progressStep;
                await Task.Yield();
                onLoadingProgressUpd?.Invoke(tasksProgress);
                await Task.Yield();
            }
        }

        onLoadingFinish?.Invoke();
    }
}
