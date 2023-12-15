using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 3;
    [SerializeField] private Vector3 rotateVectors;

    private void Update()
    {
        transform.localEulerAngles += rotateVectors * rotateSpeed * Time.deltaTime;
    }
}
