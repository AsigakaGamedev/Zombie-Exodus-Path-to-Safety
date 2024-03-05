using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnterActivator : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("is_first_enter", 1) == 1);

        PlayerPrefs.SetInt("is_first_enter", 0);
    }
}
