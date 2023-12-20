using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Прогресс загрузки: " + (progress * 100) + "%");

            await Task.Yield();
        }

        if (tasks != null)
        {
            foreach (Task task in tasks)
            {
                await task;
            }
        }

        Debug.Log("Сцена загружена!");
        screenObject.SetActive(false);
    }
}
