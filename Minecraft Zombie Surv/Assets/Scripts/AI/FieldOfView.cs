using NaughtyAttributes;
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
        
        if (colliders.Length > 0)
        {
            Debug.DrawLine(transform.position, colliders[0].transform.position + new Vector3(0, 1, 0), Color.red);
        }

        foreach (Collider collider in colliders)
        {
            float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
            if (distanceToTarget <= closestVisionRadius)
            {
                return collider.transform;
            }

            Vector3 directionToCollider = (collider.transform.position + new Vector3(0, 1, 0)) - transform.position;
            directionToCollider.Normalize();
            float angle = Vector3.Angle(transform.forward, directionToCollider);

            if (angle <= visionAngle * 0.5f)
            {
                Ray obstRay = new Ray(transform.position, directionToCollider);

                if (!Physics.Raycast(obstRay, out RaycastHit obstHit, distanceToTarget, obstacleLayerMask))
                {
                    return collider.transform;
                }
            }
        }

        return null; // Возвращаем null, если не нашли подходящий коллайдер
    }
}
