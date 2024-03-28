using System.Collections;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.SceneManagement;

public enum AudioType { Music, Effects, Characters}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<AudioType, AudioSourceContainer> sources = new SerializedDictionary<AudioType, AudioSourceContainer>();

    private void Awake()
    { 
        foreach (var source in sources)
        {
            source.Value.Init();
            source.Value.transform.SetParent(Camera.main.transform);
        }
    }

    private void Update()
    {
        foreach (var source in sources)
        {
            source.Value.UpdateContainer();
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