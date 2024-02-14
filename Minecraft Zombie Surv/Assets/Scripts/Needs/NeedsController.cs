using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, NeedData> allNeeds;

    [Space]
    [SerializeField] private float needsChangeDelay = 1; 

    public void Init()
    {
        foreach (NeedData needsData in allNeeds.Values)
        {
            needsData.Value = needsData.MaxValue;
        }

        StartCoroutine(EStartNeedsChange());
    }

    private IEnumerator EStartNeedsChange()
    {
        while (true)
        {
            foreach (NeedData needsData in allNeeds.Values)
            {
                needsData.Value += needsData.ChangeValue;
            }

            yield return new WaitForSeconds(needsChangeDelay);
        }
    }

    public NeedData GetNeed(string id)
    {
        return allNeeds[id];
    }
}
