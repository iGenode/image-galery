using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ShowImage : MonoBehaviour
{
    void Start()
    {
        GetComponent<RawImage>().texture = ReferenceManager.SelectedImage.texture;
    }
}
