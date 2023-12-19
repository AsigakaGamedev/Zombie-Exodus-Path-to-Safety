using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartRotation : MonoBehaviour
{
    [SerializeField] private Vector2 randYRotation;

    private void Start()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(randYRotation.x, randYRotation.y), 0);
    }
}
