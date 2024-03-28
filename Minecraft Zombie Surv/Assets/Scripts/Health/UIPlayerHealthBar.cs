using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIPlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private PlayerController player;
    private HealthComponent health;

    [Inject]
    private void Construct(PlayerController playerController)
    {
        this.player = playerController;
    }

    private void Start()
    {
        health = player.Health;

        slider.maxValue = health.MaxHealth;
        slider.value = health.Health;

        health.onHealthChange += OnHealthChange;
        health.onMaxHealthChange += OnMaxHealthChange;
    }

    private void OnEnable()
    {
        if (!health)
        {
            health = player.Health;
        }

        slider.maxValue = health.MaxHealth;
        slider.value = health.Health;

        health.onHealthChange += OnHealthChange;
        health.onMaxHealthChange += OnMaxHealthChange;
    }

    private void OnDisable()
    {
        if (!health) return;

        health.onHealthChange -= OnHealthChange;
        health.onMaxHealthChange -= OnMaxHealthChange;
    }

    private void OnHealthChange(float health)
    {
        slider.value = health;
    }

    private void OnMaxHealthChange(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }
}
