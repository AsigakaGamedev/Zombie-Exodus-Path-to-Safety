using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTask : TaskObject
{
    [SerializeField] private List<HealthComponent> enemiesHealth;

    public override void OnInit()
    {
        foreach (HealthComponent enemy in enemiesHealth)
        { 
           // enemy.onDie += OnKill;
        }
    }

    private void OnKill(HealthComponent health)
    {
        enemiesHealth.Remove(health);
      //  health.onDie -= OnKill;

        if (enemiesHealth.Count == 0)
        {
            onFinish?.Invoke(this);
        }
    }
}
