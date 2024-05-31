using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence<T> where T: class
{
    void LoadData(T data);

    void SaveData(ref T data);
}
