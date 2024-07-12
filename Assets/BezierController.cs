using UnityEngine;
using UnityEngine.UI;

public class BezierController : MonoBehaviour
{
    [SerializeField] private BeizerLine[] _bezierLines = null;
    [SerializeField] private Slider _slider = null; 
    [SerializeField] private SceneNode _mainMover = null;

    private LineRenderer _lineRenderer = null;
    private Vector3 _bufferPosition = Vector3.zero;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(delegate {UpdateMovers();});

        foreach (var bezierLine in _bezierLines)
        {
            bezierLine.Origin.OnNodeDragAction += OnNodeDragActionHandler;
            bezierLine.End.OnNodeDragAction += OnNodeDragActionHandler;
        }
    }

    private void OnDisable()
    {
        foreach (var bezierLine in _bezierLines)
        {
            bezierLine.Origin.OnNodeDragAction -= OnNodeDragActionHandler;
            bezierLine.End.OnNodeDragAction -= OnNodeDragActionHandler;
        }
    }
    
    private void OnNodeDragActionHandler()
    {
        int sliderValue = (int)_slider.value;

        foreach(var bezierLine in _bezierLines)
        {
            bezierLine.DrawLine();
            bezierLine.UpdateMover(sliderValue);
        }
    }

    private void UpdateMovers()
    {
        int sliderValue = (int)_slider.value;

        foreach(var bezierLine in _bezierLines)
        {
            bezierLine.DrawLine();
            bezierLine.UpdateMover(sliderValue);
        }

        _lineRenderer.positionCount = 51;
        _lineRenderer.SetPosition(sliderValue, _mainMover.transform.position);
    }
}