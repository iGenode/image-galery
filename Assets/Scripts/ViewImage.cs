using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ViewImage : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ReferenceManager.SelectedImage = GetComponent<RawImage>();
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
