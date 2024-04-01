using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public List<GameObject> backgrounds;

    void Start()
    {
        int randomIndex = Random.Range(0, backgrounds.Count);

        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].SetActive(i == randomIndex);
        }
    }
}

