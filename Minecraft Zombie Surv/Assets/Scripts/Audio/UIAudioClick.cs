using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIAudioClick : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioType type;

    [Space]
    [SerializeField] private Button btn;

    private AudioManager audioManager;

    private void OnValidate()
    {
        if (!btn) btn = GetComponent<Button>();
    }

    [Inject]
    private void Construct(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            audioManager.PlayAudio(clip, type);
        });
    }
}