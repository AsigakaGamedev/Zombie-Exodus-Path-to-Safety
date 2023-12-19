using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksHandler : MonoBehaviour
{
    [SerializeField] private BaseAttack[] allAttacks;

    [Space]
    [SerializeField] private float timeBetweenAttacks;

    private bool canAttack;

    public void Init()
    {
        canAttack = true;

        foreach (var attack in allAttacks)
        {
            attack.OnInit();
        }
    }

    public bool TryAttack()
    {
        if (!canAttack) return false;

        foreach (var attack in allAttacks)
        {
            attack.OnAttack();
        }

        canAttack = false;
        Invoke(nameof(ResetCanAttack), timeBetweenAttacks);
        return true;
    }

    private void ResetCanAttack()
    {
        canAttack = true;
    }
}
