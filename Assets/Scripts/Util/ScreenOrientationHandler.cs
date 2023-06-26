using UnityEngine;

public static class ScreenOrientationHandler
{
    public static void HandleOrientationForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Main Menu":
            case "Gallery":
                Debug.Log($"In switch Main menu or gallery");
                Screen.orientation = ScreenOrientation.Portrait;
                ChangeAutoRotate(false);
                break;
            case "View":
                Debug.Log($"In switch View");
                Screen.orientation = ScreenOrientation.AutoRotation;
                ChangeAutoRotate(true);
                break;
        }
    }

    private static void ChangeAutoRotate(bool shouldAutoRotate)
    {
        Debug.Log($"Setting auto rotation to {shouldAutoRotate}");
        Screen.autorotateToPortrait = shouldAutoRotate;
        Screen.autorotateToPortraitUpsideDown = shouldAutoRotate;
        Screen.autorotateToLandscapeLeft = shouldAutoRotate;
        Screen.autorotateToLandscapeRight = shouldAutoRotate;
    }
}
