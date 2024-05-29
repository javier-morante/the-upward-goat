using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void NextScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void NextScene(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }
}
