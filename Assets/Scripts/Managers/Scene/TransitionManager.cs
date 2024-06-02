using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TransitionManager : MonoBehaviour
{
    // Singleton instance of the TransitionManager
    private static TransitionManager _instance;
    public static TransitionManager instance
    {
        get
        {
            return _instance;
        }
    }

    // Animator reference for scene transition animations
    private Animator animator;
    private int hashShowAnim = Animator.StringToHash("Show");
    private int hashShow2Anim = Animator.StringToHash("Show2");

    // Initialize the singleton instance
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
        }
        else if (_instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Initialize the animator component and ensure it persists across scenes
    void Init()
    {
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    // UI elements for the progress bar
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    // Load a scene with a progress bar by scene name
    public void LoadSceneWithProgressBar(SceneName sceneName)
    {
        StartCoroutine(LoadCoroutineWithProgressBar(sceneName.ToString()));
    }

    // Load a scene with a progress bar by scene name (string)
    public void LoadSceneWithProgressBar(string sceneName)
    {
        StartCoroutine(LoadCoroutine(sceneName));
    }

    // Load a scene without a progress bar by scene name
    public void LoadScene(SceneName sceneName)
    {
        StartCoroutine(LoadCoroutine(sceneName.ToString()));
    }

    // Load a scene without a progress bar by scene name (string)
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadCoroutineWithProgressBar(sceneName));
    }

    // Coroutine to load a scene without a progress bar
    IEnumerator LoadCoroutine(string sceneName)
    {
        // Trigger the transition animation
        animator.SetBool(hashShow2Anim, true);
        // Wait for a short delay before loading the scene
        yield return new WaitForSecondsRealtime(0.50f);
        // Load the scene asynchronously
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the scene is fully loaded
        while (!sceneAsync.isDone)
        {
            yield return null;
        }
        // Turn off the transition animation
        animator.SetBool(hashShow2Anim, false);
    }

    // Coroutine to load a scene with a progress bar
    IEnumerator LoadCoroutineWithProgressBar(string sceneName)
    {
        // Trigger the transition animation
        animator.SetBool(hashShowAnim, true);
        // Initialize the progress bar
        UpdateProgressValue(0);
        // Wait for a short delay before loading the scene
        yield return new WaitForSecondsRealtime(0.50f);
        // Load the scene asynchronously
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Update the progress bar until the scene is fully loaded
        while (!sceneAsync.isDone)
        {
            UpdateProgressValue(sceneAsync.progress);
            yield return null;
        }
        // Set the progress bar to full and turn off the transition animation
        UpdateProgressValue(1);
        animator.SetBool(hashShowAnim, false);
    }

    // Update the progress bar value and progress text
    void UpdateProgressValue(float progressValue)
    {
        if (progressBar != null)
            progressBar.value = progressValue;
        if (progressText != null)
            progressText.text = $"{(int)(progressValue * 100)}%";
    }
}


