using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonologTrigger : MonoBehaviour
{
    [SerializeField] private List<MonologData> monologList;

    private MonologsManager monologsManager;

    [Inject]
    private void Construct(MonologsManager monologsManager)
    {
        this.monologsManager = monologsManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monologsManager.SetMonolog(monologList);
            Destroy(gameObject);
        }
    }
}
