using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IObserver<T> where T : Enum
{
    public void OnNotify(T notification);
}
