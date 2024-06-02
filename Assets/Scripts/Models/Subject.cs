using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Subject<T> class implements the observer pattern, allowing objects to subscribe and be notified of changes.
// It requires T to be an Enum, ensuring type safety for notifications.
public class Subject<T> : MonoBehaviour where T : Enum
{
    // List of observers subscribing to notifications of type T
    private List<IObserver<T>> _observers = new List<IObserver<T>>();

    // Method to remove an observer from the list
    public void RemoveObserver(IObserver<T> observer)
    {
        _observers.Remove(observer);
    }

    // Method to add an observer to the list
    public void AddObserver(IObserver<T> observer)
    {
        _observers.Add(observer);
    }

    // Method to notify all observers of a given notification
    public void NotifyObservers(T notification)
    {
        // Iterate through the list of observers and call their OnNotify method with the notification
        _observers.ForEach((observer) =>
        {
            observer.OnNotify(notification);
        });
    }
}

