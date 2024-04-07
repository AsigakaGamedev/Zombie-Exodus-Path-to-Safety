using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskLevelManager : MonoBehaviour
{
    [SerializeField] private List<TaskObject> tasks;

    private void Start()
    {
        foreach (TaskObject task in tasks)
        {
            task.OnInit();
            task.onFinish += OnFinishTask;
        }
    }

    private void OnFinishTask(TaskObject task)
    {
        tasks.Remove(task);
        task.onFinish -= OnFinishTask;

        if (tasks.Count == 0)
        {
            print("You win");
        }
    }
}
