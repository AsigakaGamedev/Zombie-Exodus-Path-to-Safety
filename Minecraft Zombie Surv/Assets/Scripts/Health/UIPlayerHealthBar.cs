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
        health = ServiceLocator.GetService<PlayerController>().Health;
        slider.maxValue = health.MaxHealth;
        slider.value = health.Health;

        health.onHealthChange += OnHealthChange;
        health.onMaxHealthChange += OnMaxHealthChange;
    }

    private void OnDestroy()
    {
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
