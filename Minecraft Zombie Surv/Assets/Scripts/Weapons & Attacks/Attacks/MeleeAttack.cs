using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    [SerializeField] private float attackRadius = 7f;
    [SerializeField] private float attackAngle = 70f;
    [SerializeField] private float damage = 10;

    [Space]
    [SerializeField] private LayerMask targetLayerMask;

    private void OnDrawGizmosSelected()
    {
        // Отрисовка гизмоса для радиуса
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        // Отрисовка гизмоса для угла обзора
        Vector3 fovLine1 = Quaternion.AngleAxis(attackAngle * 0.5f, transform.up) * transform.forward * attackRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-attackAngle * 0.5f, transform.up) * transform.forward * attackRadius;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }

    public override void OnAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius, targetLayerMask);

        foreach (Collider collider in colliders)
        {
            if (!collider.TryGetComponent(out HealthComponent health)) continue;

            Vector3 directionToCollider = (collider.transform.position + new Vector3(0, 1, 0)) - transform.position;
            directionToCollider.Normalize();
            float angle = Vector3.Angle(transform.forward, directionToCollider);

            if (angle <= attackAngle * 0.5f)
            {
                health.ChangeHealth(-damage);
            }
        }
    }
}
