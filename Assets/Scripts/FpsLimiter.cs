using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLijmiter : MonoBehaviour
{
    [SerializeField] int targetFps = 30;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFps;
    }
}
