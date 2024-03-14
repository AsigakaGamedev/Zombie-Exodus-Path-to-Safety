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
    [ReadOnly, SerializeField] private float health;

    public Action<float> onHealthChange;
    public Action<float> onMaxHealthChange;
    public Action onDie;
    public Action onDamage;
    public Action onHeal;

    public float MaxHealth { get => maxHealth; }
    public float Health { get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, maxHealth);

            if (health <= 0)
            {
                Kill();
            }
        }
    }

    private void Start()
    {
        Health = maxHealth;
        onHealthChange?.Invoke(health);
    }

    public void ChangeHealth(float increaseValue)
    {
        Health += increaseValue;
        onHealthChange?.Invoke(health);

        if (increaseValue > 0)
        {
            onHeal?.Invoke();
        }
        else if (increaseValue < 0)
        {
            onDamage?.Invoke();
        }
    }

    [Button]
    public void Kill()
    {
        health = 0;
        onDie?.Invoke();
        if (deactivateOnDie) gameObject.SetActive(false);

        if (ragdoll) ragdoll.Activate();
    }
}
