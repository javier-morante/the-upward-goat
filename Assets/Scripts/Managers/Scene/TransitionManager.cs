using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TransitionManager : MonoBehaviour
{
   private static TransitionManager _instance;
   public static TransitionManager instance{
    get{
        return _instance;
    }
   }

   private Animator animator;
   private int hashShowAnim = Animator.StringToHash("Show");
   private int hashShow2Anim = Animator.StringToHash("Show2");

   void Awake(){
    if (_instance == null)
    {
        _instance = this;
        Init();
    }else if (_instance != null)
    {
        Destroy(gameObject);
    }
   }

   void Init(){
    animator = GetComponent<Animator>();
    DontDestroyOnLoad(gameObject);
   }


   [SerializeField] private Slider progressBar;
   [SerializeField] private TextMeshProUGUI progressText;


    public void LoadSceneWithProgressBar(SceneName sceneName){
        
        StartCoroutine(LoadCoroutineWithProgressBar(sceneName.ToString()));
    }

    public void LoadSceneWithProgressBar(string sceneName){
        
        StartCoroutine(LoadCoroutine(sceneName));
    }

    public void LoadScene(SceneName sceneName){
        
        StartCoroutine(LoadCoroutine(sceneName.ToString()));
    }

    public void LoadScene(string sceneName){
        
        StartCoroutine(LoadCoroutineWithProgressBar(sceneName));
    }

    IEnumerator LoadCoroutine(string sceneName){
        animator.SetBool(hashShow2Anim,true);
        yield return new WaitForSecondsRealtime(0.50f);
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Single);
        while (!sceneAsync.isDone)
        {
            yield return null;
        }
        animator.SetBool(hashShow2Anim,false);
    }

    IEnumerator LoadCoroutineWithProgressBar(string sceneName){
        animator.SetBool(hashShowAnim,true);
        UpdateProgressValue(0);
        yield return new WaitForSecondsRealtime(0.50f);
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Single);
        while (!sceneAsync.isDone)
        {
            UpdateProgressValue(sceneAsync.progress);
            yield return null;
        }
        UpdateProgressValue(1);
        animator.SetBool(hashShowAnim,false);
    }

    void UpdateProgressValue(float progressValue){
        if (progressBar != null)
        progressBar.value = progressValue;
        if (progressText.text != null)
        progressText.text = $"{(int)progressValue*100}%";
    }

}
