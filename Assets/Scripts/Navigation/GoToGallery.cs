using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GoToGallery : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            NavigationManager.Instance.GoToGallery();
        });
    }
}
