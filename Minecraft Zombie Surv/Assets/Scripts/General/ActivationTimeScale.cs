using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTimeScale : MonoBehaviour
{
    [Range(0, 1), SerializeField] private float enableTimeScale;
    [Range(0, 1), SerializeField] private float disableTimeScale;

    private void OnEnable()
    {
        Time.timeScale = enableTimeScale;
    }

    private void OnDisable()
    {
        Time.timeScale = disableTimeScale;
    }
}
