using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class DynamicGridSizeScaler : MonoBehaviour
{
    void Awake()
    {
        // Getting grid layout and its width
        var gridLayout = GetComponent<GridLayoutGroup>();
        var width = GetComponent<RectTransform>().rect.width;

        // Calculating spacing and column count to use when scaling cell size
        var horizontalSpacing = gridLayout.spacing.x * 1.5f;
        var columnCount = gridLayout.constraintCount;

        // Calculating and setting cell size
        var cellSize = new Vector2(
            width / columnCount - horizontalSpacing,
            width / columnCount - horizontalSpacing
        );
        gridLayout.cellSize = cellSize;
    }
}
