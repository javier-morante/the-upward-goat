using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Method to hide or show the cursor
    public static void HideCursor(bool hide)
    {
        // Lock or unlock the cursor based on the 'hide' parameter
        Cursor.lockState = hide ? CursorLockMode.Locked : CursorLockMode.None;
        // Set the visibility of the cursor based on the inverse of the 'hide' parameter
        Cursor.visible = !hide;
    }

    // Method called before the splash screen is displayed
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void OnBeforeSplashScreen()
    {
        // Hide the cursor when the game starts
        HideCursor(true);
    }
}

