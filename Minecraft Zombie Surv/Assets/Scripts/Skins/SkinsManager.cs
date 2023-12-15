using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    [SerializeField] private Texture2D startTexture;
    [SerializeField] private Texture2D[] allTetxures;

    [Space]
    [SerializeField] private Texture2D selectedTexture;

    public Action<Texture2D> onSelectSkin;

    public Texture2D[] AllTetxures { get => allTetxures; }
    public Texture2D SelectedTexture { get => selectedTexture; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Awake()
    {
        SelectSkin(startTexture);
    }

    public void SelectSkin(Texture2D texture)
    {
        selectedTexture = texture;
        onSelectSkin?.Invoke(selectedTexture);
    }
}
