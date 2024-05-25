using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class assa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        string[] a = Directory.GetFiles("D:/Unity/UnityProjects/TheUpwardGoat/Assets/Scripts/Camera");
        foreach (var item in a)
        {
            Debug.Log(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
