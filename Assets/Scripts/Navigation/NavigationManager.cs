using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance;

    private Stack<SceneData> _backStack = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                OnBackPress();
            }
        }
    }

    public void OnBackPress()
    {
        if (_backStack.Count != 0)
        {
            var data = _backStack.Pop();
            if (data.IsNextSceneSingle)
            {
                SceneManager.LoadScene(data.SceneFrom);
            }
            else
            {
                SceneManager.UnloadSceneAsync(data.SceneTo);
            }
            ScreenOrientationHandler.HandleOrientationForScene(data.SceneFrom);
        }
        else
        {
            Application.Quit();
        }
    }

    public void GoToGallery()
    {
        LoadSceneWithLoadingScreen("Gallery");
    }

    public void GoToViewImage()
    {
        LoadSceneWithLoadingScreen("View", LoadSceneMode.Additive);
    }

    private void LoadSceneWithLoadingScreen(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        // Add current scene to back stack
        _backStack.Push(new SceneData(SceneManager.GetActiveScene().name, sceneName, mode == LoadSceneMode.Single));
        // Load a new scene
        LoadManager.Instance.LoadSceneAsync(sceneName, mode);
        // Handle orientation changes
        ScreenOrientationHandler.HandleOrientationForScene(sceneName);
    }

    /// <summary>
    /// Class to hold back stack entry data
    /// </summary>
    private class SceneData
    {
        /// <summary>
        /// Name of the previous scene that is added to back stack
        /// </summary>
        public string SceneFrom;
        /// <summary>
        /// Name of the scene that is being navigated to
        /// </summary>
        public string SceneTo;
        /// <summary>
        /// Is next scene loaded in single mode
        /// </summary>
        public bool IsNextSceneSingle;

        public SceneData(string sceneFrom, string sceneTo, bool isNextSceneSingle)
        {
            SceneFrom = sceneFrom;
            SceneTo = sceneTo;
            IsNextSceneSingle = isNextSceneSingle;
        }
    }
}
