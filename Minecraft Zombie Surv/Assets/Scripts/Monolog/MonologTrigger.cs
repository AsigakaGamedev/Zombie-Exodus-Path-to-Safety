using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTrigger : MonoBehaviour
{
    private MonologsController monologsController;
    [SerializeField] private List<MonologData> monologList;

    private void Start()
    {
        monologsController = ServiceLocator.GetService<MonologsController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartMonolog();
        }
    }


    private void StartMonolog()
    {
        Debug.Log("Игрок вошел в триггер! Выполняем ваш код.");
        monologsController.SetMonolog(monologList);
       
    }
}
