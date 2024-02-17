using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceContainer : MonoBehaviour
{
    [SerializeField] private AudioSource[] sources;

    [Space]
    [Range(0, 1), SerializeField] private float volume;

    public Action<float> onVolumeChanged;

    public float Volume { get => volume; }

    private void OnValidate()
    {
        SetVolume(volume);
    }

    public void PlayAudio(AudioClip clip)
    {
        foreach (var source in sources)
        {
            if (source.clip != null) continue;

            source.clip = clip;
            source.Play();
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
