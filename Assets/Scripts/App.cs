using UnityEngine;

public class App : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ScreenOrientationHandler.HandleOrientationForScene("Main Menu");
    }
}
