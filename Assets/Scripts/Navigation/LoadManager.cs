using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public static LoadManager Instance;

    [Header("Loading UI")]
    [SerializeField]
    private GameObject _loadingCanvas;

    [Header("Progress Elements")]
    [SerializeField]
    private Slider _progressBar;
    [SerializeField]
    private TextMeshProUGUI _progressText;

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

    public async void LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        var asyncScene = SceneManager.LoadSceneAsync(sceneName, mode);
        asyncScene.allowSceneActivation = false;

        _loadingCanvas.SetActive(true);

        var progress = 0.0f;

        do
        {
            _progressBar.value = progress;
            _progressText.text = Mathf.Round(progress * 100f) + "%";

            progress += .01f;

            await Task.Delay(10);
        } 
        while (progress <= 1f);

        asyncScene.allowSceneActivation = true;
        StartCoroutine(WaitToHideLoading(1));
    }

    private IEnumerator WaitToHideLoading(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        _loadingCanvas.SetActive(false);
        _progressBar.value = 0;
    }
}
