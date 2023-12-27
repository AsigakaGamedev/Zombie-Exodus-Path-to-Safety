using System.Collections;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }
}