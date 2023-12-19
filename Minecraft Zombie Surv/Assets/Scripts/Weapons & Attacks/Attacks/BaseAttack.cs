using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    public virtual void OnInit() { }

    public abstract void OnAttack();
}
