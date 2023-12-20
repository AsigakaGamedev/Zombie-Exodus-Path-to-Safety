using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float frequency = 0.5f;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > frequency)
        {
            float fps = 1f / Time.deltaTime;
            textMesh.text = $"FPS: {(int)Mathf.Round(fps)}";

            timer = 0f;
        }
    }
}
