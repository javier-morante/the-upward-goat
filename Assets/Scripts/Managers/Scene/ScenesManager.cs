using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void NextScene(string sceneName){
        DataPersistanceManager.instance.SaveGameData();
        DataPersistanceManager.instance.SaveSettings();
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void NextScene(int sceneIndex){
        DataPersistanceManager.instance.SaveGameData();
        DataPersistanceManager.instance.SaveSettings();
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void NextSceneWithOutSave(string sceneIndex){
        
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
