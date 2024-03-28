using System.Collections;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.SceneManagement;

public enum AudioType { Music, Effects, Characters}

public class AudioManager : AInitializable
{
    [SerializeField] private SerializedDictionary<AudioType, AudioSourceContainer> sources = new SerializedDictionary<AudioType, AudioSourceContainer>();

    public override void OnInit()
    { 
        foreach (var source in sources)
        {
            source.Value.Init();
            source.Value.transform.SetParent(Camera.main.transform);
        }

        StartCoroutine(EUpdateSources());
    }

    private IEnumerator EUpdateSources()
    {
        while (true)
        {
            foreach (var source in sources)
            {
                source.Value.UpdateContainer();
            }

            yield return new WaitForEndOfFrame();
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