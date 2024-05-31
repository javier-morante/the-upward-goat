using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static void HideCursor(bool hide){
        Cursor.lockState = hide?CursorLockMode.Locked:CursorLockMode.None;
        Cursor.visible = hide;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void OnBeforeSplashScreen(){
        HideCursor(true);
    }
}
