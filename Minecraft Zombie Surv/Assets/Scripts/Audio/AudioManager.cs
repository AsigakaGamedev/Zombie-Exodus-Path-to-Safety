using System.Collections;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.SceneManagement;

public enum AudioType { Music, Effects, Characters}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<AudioType, AudioSourceContainer> sources = new SerializedDictionary<AudioType, AudioSourceContainer>();

    private LoadingManager loadingManager;

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
        loadingManager = ServiceLocator.GetService<LoadingManager>();
        loadingManager.onLoadingStart += OnLoadingStart;
        loadingManager.onLoadingFinish += OnLoadingFinish;

        foreach (var source in sources)
        {
            source.Value.transform.SetParent(Camera.main.transform);
        }
    }

    private void OnDestroy()
    {
        loadingManager.onLoadingStart -= OnLoadingStart;
        loadingManager.onLoadingFinish -= OnLoadingFinish;
    }

    private void OnLoadingStart()
    {
        foreach (var source in sources)
        {
            source.Value.transform.parent = transform;
        }
    }

    private void OnLoadingFinish()
    {
        foreach (var source in sources)
        {
            source.Value.transform.parent = Camera.main.transform;
            source.Value.transform.localPosition = Vector3.zero;
            source.Value.transform.localRotation = Quaternion.identity;
            source.Value.transform.localScale = Vector3.one;
        }
    }

    public void PlayAudio(AudioClip clip, AudioType type)
    {
        sources[type].PlayAudio(clip);
    }

    public void SetVolume(float volume, AudioType type)
    {
        sources[type].SetVolume(volume);
    }

    public AudioSourceContainer GetSource(AudioType type)
    {
        return sources[type];
    }
}