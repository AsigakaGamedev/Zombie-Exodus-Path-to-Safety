using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private HealthComponent health;

    private void Start()
    {
        PlayerController player = ServiceLocator.GetServiceSafe<PlayerController>();

        if (!player) return;

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
            PlayerController player = ServiceLocator.GetServiceSafe<PlayerController>();

            if (!player) return;

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
