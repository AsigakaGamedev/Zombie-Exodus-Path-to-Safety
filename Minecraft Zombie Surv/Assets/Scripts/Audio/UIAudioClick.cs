using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        audioManager = ServiceLocator.GetService<AudioManager>();

        btn.onClick.AddListener(() =>
        {
            audioManager.PlayAudio(clip, type);
        });
    }
}