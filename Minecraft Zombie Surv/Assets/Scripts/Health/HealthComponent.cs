using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private bool deactivateOnDie = false;
    [SerializeField] private RagdollController ragdoll;

    [Space]
    [SerializeField] private Slider slider;

    [Space]
    [ReadOnly, SerializeField] private float health;

    public Action<float> onDamage;
    public Action onDie;

    private void Start()
    {
        health = maxHealth;
        SetMaxValue(health);
    }

    public void Damage(float damage)
    {
        if (health <= 0) return;

        health -= damage;
        onDamage?.Invoke(health);
        SetValue(health);

        if (health <= 0)
        {
            onDie?.Invoke();
            if (deactivateOnDie) gameObject.SetActive(false);

            if (ragdoll) ragdoll.Activate();
        }
    }

    public void SetMaxValue(float maxValue)
    {
        if (slider)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }
    }

    public void SetValue(float value)
    {
        if (slider)
        {
            slider.value = value;
        }
    }
}
