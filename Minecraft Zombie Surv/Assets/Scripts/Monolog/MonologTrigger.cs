using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTrigger : MonoBehaviour
{
    [SerializeField] private List<MonologData> monologList;

    private MonologsManager monologsController;

    private void Start()
    {
        monologsController = ServiceLocator.GetService<MonologsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monologsController.SetMonolog(monologList);
            Destroy(gameObject);
        }
    }
}
