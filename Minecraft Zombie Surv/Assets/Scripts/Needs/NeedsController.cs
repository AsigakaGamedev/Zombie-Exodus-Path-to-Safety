using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, NeedData> allNeeds;

    [Space]
    [SerializeField] private float needsChangeDelay = 1; 

    private void Start()
    {
        StartCoroutine(EStartNeedsChange());
    }

    private IEnumerator EStartNeedsChange()
    {
        foreach (NeedData needsData in allNeeds.Values)
        {
            needsData.Value -= needsData.ChangeValue * Time.deltaTime;
        }

        yield return new WaitForSeconds(needsChangeDelay);
    }

    public NeedData GetNeed(string id)
    {
        return allNeeds[id];
    }
}
