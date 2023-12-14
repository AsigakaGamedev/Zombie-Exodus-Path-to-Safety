using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateBase : MonoBehaviour
{
    public virtual void OnInit() { }
    public virtual bool OnValidateState() { return true; }
    public virtual void OnEnterState() { }
    public virtual void OnUpdateState() { }
    public virtual void OnExitState() { }
}
