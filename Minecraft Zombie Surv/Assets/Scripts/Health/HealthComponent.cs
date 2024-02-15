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

    public float MaxHealth { get => maxHealth; }
    public float Health { get => health; }

    private void Start()
    {
        health = maxHealth;
    }

    public void Damage(float damage)
    {
        if (health <= 0) return;

        health -= damage;
        onHealthChange?.Invoke(health);

        if (health <= 0)
        {
            Kill();
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
