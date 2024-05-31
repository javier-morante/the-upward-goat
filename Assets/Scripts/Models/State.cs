using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class State<T> where T : MonoBehaviour
{
    protected T machine;
    public State(T machine){
        this.machine = machine;
    }

    public virtual void OnEnter() { }

    public virtual void OnUpdate() { }

    public virtual void OnPhysicsUpdate() { }

    public virtual void OnExit() { }

}
