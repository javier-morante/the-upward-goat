using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject<T> : MonoBehaviour where T :Enum
{
    private List<IObserver<T>> _observers = new List<IObserver<T>>();

    public void RemoveObserver(IObserver<T> observer){
        _observers.Remove(observer);
    }

    public void AddObserver(IObserver<T> observer){
        _observers.Add(observer);
    }

    public void NotifyObservers(T notification){
        _observers.ForEach((observer)=>{
            observer.OnNotify(notification);
        });
    }
}
