using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIAudioSettings : MonoBehaviour
{
    [SerializeField] private AudioType audioType;

    [Space]
    [SerializeField] private Button volumeBtn;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;

    private AudioManager audioManager;
    private AudioSourceContainer audioSource;

    [Inject]
    private void Construct(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }

    private void Start()
    {
        audioSource = audioManager.GetSource(audioType);

        volumeBtn.onClick.AddListener(() =>
        {
            audioManager.SetVolume(0, audioType);
        });

        volumeSlider.onValueChanged.AddListener((float volume) =>
        {
            audioManager.SetVolume(volume, audioType);
        });

        audioSource.onVolumeChanged += OnVolumeChanged;

        volumeSlider.value = audioSource.Volume;
        volumeText.text = $"{(int)(audioSource.Volume * 100)}%";
    }

    private void OnDestroy()
    {
        audioSource.onVolumeChanged -= OnVolumeChanged;
    }

    private void OnVolumeChanged(float volume)
    {
        volumeSlider.value = volume;
        volumeText.text = $"{(int)(volume * 100)}%";
    }
}
