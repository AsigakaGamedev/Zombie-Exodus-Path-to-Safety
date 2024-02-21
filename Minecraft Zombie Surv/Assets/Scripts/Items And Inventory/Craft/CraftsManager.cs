using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftsManager : MonoBehaviour
{
    [SerializeField] private CraftInfo[] allCrafts;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public List<CraftInfo> GetCrafts(CraftType craftType)
    {
        List<CraftInfo> typeCrafts = new List<CraftInfo>();

        foreach (CraftInfo craft in allCrafts)
        {
            if (craft.CraftType == craftType) typeCrafts.Add(craft); 
        }

        return typeCrafts;
    }
}
