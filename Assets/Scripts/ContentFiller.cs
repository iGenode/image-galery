using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContentFiller : MonoBehaviour
{
    [SerializeField]
    private RawImage _gridCell;

    [SerializeField]
    private ScrollRect _scrollRect;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup;

    private int _lastItemIndex = 1;
    private int _columnCount;
    private int _runningCoroutineCount = 0;

    private void Start()
    {
        _columnCount = _gridLayoutGroup.constraintCount;

        FetchInitialData();

        _scrollRect.onValueChanged.AddListener((position) =>
        {
            if (_scrollRect.verticalNormalizedPosition <= 0.25f 
                && _scrollRect.verticalNormalizedPosition >= 0 
                && _lastItemIndex != -1 
                && _runningCoroutineCount == 0)
            {
                // Fetch more data
                StartCoroutine(FetchAnAmountOfData(_columnCount));
                //Debug.Log($"Should fetch more data, {_scrollRect.verticalNormalizedPosition}");
            }
            //Debug.Log($"Position at {_scrollRect.verticalNormalizedPosition}");
        });
        //_scrollRect.on
    }

    private void FetchInitialData()
    {
        var gridRectTransform = GetComponent<RectTransform>();
        // Calculating the amount of elements that fit the screen
        var amount = Mathf.FloorToInt(gridRectTransform.rect.height / _gridLayoutGroup.cellSize.x) * _columnCount;
        StartCoroutine(FetchAnAmountOfData(amount));
    }

    private IEnumerator FetchAnAmountOfData(int amount)
    {
        for (int i = _lastItemIndex; i < _lastItemIndex + amount; i++)
        {
            StartCoroutine(
                ImageService.LoadImage(
                    $"http://data.ikppbb.com/test-task-unity-data/pics/{i}.jpg",
                    CreateCell
                )
            );
            _runningCoroutineCount++;
        }
        while (_runningCoroutineCount > 0)
        {
            yield return null;
        }
        _lastItemIndex += amount;
        //Debug.Log($"Last item index after starting fetching is {_lastItemIndex}");
    } 

    private void CreateCell(Texture texture)
    {
        _runningCoroutineCount--;
        if (texture == null)
        {
            // Stop trying to fetch, reached the end or got an error
            _lastItemIndex = -1;
            // TODO: might want to also remove listener of scrollRect
        }
        else
        {
            Instantiate(_gridCell, _gridLayoutGroup.transform).texture = texture;
        }
    }
}
