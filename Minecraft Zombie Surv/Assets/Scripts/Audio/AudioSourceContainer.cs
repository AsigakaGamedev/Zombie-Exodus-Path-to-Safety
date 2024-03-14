using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioSourceContainer : MonoBehaviour
{
    [SerializeField] private AudioSource[] sources;

    [Space]
    [Range(0, 1), SerializeField] private float volume;

    private float[] sourcesDelays;

    public Action<float> onVolumeChanged;

    public float Volume { get => volume; }

    private void OnValidate()
    {
        SetVolume(volume);
    }

    public void Init()
    {
        sourcesDelays = new float[sources.Length];
    }

    public void UpdateContainer()
    {
        for (int i = 0; i < sourcesDelays.Length; i++)
        {
            if (sourcesDelays[i] <= 0) continue;

            sourcesDelays[i] -= Time.deltaTime;
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            if (sourcesDelays[i] > 0) continue;

            sources[i].clip = clip;
            sources[i].Play();
            sourcesDelays[i] = clip.length;
            break;
        }
    }

    public void ClearSources()
    {
        foreach (var source in sources)
        {
            source.clip = null;
        }
    }

    public void SetVolume(float volume)
    {
        this.volume = Mathf.Clamp01(volume);
        onVolumeChanged?.Invoke(this.volume);

        foreach (var source in sources)
        {
            source.volume = this.volume;
        }
    }
}
