using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioActivationPlayer : MonoBehaviour
{
    [SerializeField] private AudioType audioType;
    [SerializeField] private AudioClip audioClip;

    private AudioManager audioManager;

    private void Start()
    {
        try
        {
            audioManager = ServiceLocator.GetService<AudioManager>();
            audioManager.PlayAudio(audioClip, audioType);
        }
        catch { }
    }
}
