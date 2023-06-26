using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ViewImage : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ReferenceManager.SelectedImage = GetComponent<RawImage>();
        NavigationManager.Instance.GoToViewImage();
        //LoadManager.Instance.LoadSceneAsync("View", LoadSceneMode.Additive);
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
