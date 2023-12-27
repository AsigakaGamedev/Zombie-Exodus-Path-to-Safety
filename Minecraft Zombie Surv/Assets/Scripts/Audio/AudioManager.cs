using System.Collections;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public enum AudioType { Music, Effect, UI}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<AudioType, AudioSource[]> sources;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void PlayAudio(AudioClip clip, AudioType type)
    {
        foreach (var source in sources[type])
        {
            if (source.clip != null) continue;

            source.clip = clip;
            source.Play();
        }
    }
}