using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskObject : MonoBehaviour
{
    private bool isFinished;

    public Action<TaskObject> onFinish;

    public virtual void OnInit() { }

    private void FinishTask()
    {
        
    }
}
