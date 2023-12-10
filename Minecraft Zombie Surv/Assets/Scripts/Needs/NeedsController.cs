using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    [SerializeField] private List<NeedsData> needsDataList;

    void Update()
    {
        foreach (NeedsData needsData in needsDataList)
        {
            needsData.Value -= needsData.ChangeValue * Time.deltaTime;
        }
    }
}
