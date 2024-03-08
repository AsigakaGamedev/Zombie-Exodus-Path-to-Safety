using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float visionRadius = 7f;
    [SerializeField] private float visionAngle = 70f;
    [SerializeField] private float closestVisionRadius = 1f;

    [Space]
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private LayerMask obstacleLayerMask;

    private void OnDrawGizmosSelected()
    {
        // Отрисовка гизмоса для радиуса
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);

        // Отрисовка гизмоса для угла обзора
        Vector3 fovLine1 = Quaternion.AngleAxis(visionAngle * 0.5f, transform.up) * transform.forward * visionRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-visionAngle * 0.5f, transform.up) * transform.forward * visionRadius;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
        Gizmos.DrawWireSphere(transform.position, closestVisionRadius);
        //Gizmos.DrawLine(transform.position + fovLine1, transform.position + fovLine2);
    }

    public Transform DetectColliderInVision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, visionRadius, targetLayerMask);

        foreach (Collider collider in colliders)
        {
            if (Vector3.Distance(transform.position, collider.transform.position) <= closestVisionRadius)
            {
                return collider.transform;
            }

            Vector3 directionToCollider = collider.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToCollider);

            if (angle <= visionAngle * 0.5f)
            {
                if (!Physics.Raycast(transform.position, directionToCollider, visionRadius, obstacleLayerMask))
                {
                    return collider.transform;
                }
            }
        }

        return null; // Возвращаем null, если не нашли подходящий коллайдер
    }
}
