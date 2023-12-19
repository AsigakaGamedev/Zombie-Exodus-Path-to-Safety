using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAttack : BaseAttack
{
    [SerializeField] private Transform attackStartPoint;

    [Space]
    [SerializeField] private LayerMask damagableLayers;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackDamage;

    private void OnDrawGizmosSelected()
    {
        if (!attackStartPoint) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(attackStartPoint.position, attackStartPoint.forward * attackDistance);
    }

    public override void OnAttack()
    {
        Ray attackRay = new Ray(attackStartPoint.position, attackStartPoint.forward);

        if (Physics.Raycast(attackRay, out RaycastHit hit, attackDistance, damagableLayers))
        {
            if (hit.collider.TryGetComponent(out HealthComponent damageHealth))
            {
                damageHealth.Damage(attackDamage);
            }
        }
    }
}
