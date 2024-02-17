using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceContainer : MonoBehaviour
{
    [SerializeField] private AudioSource[] sources;

    [Space]
    [Range(0, 1), SerializeField] private float volume;

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
        volume = Mathf.Clamp01(volume);

        foreach (var source in sources)
        {
            source.volume = volume;
        }
    }
}
