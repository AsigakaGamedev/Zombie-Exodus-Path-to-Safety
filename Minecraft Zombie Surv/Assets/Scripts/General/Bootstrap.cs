using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AInitializable[] initializables;

    private void Awake()
    {
        foreach (AInitializable initializable in initializables)
        {
            initializable.OnInit();
        }
    }
}
