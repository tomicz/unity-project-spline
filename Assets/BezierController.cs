using UnityEngine;
using UnityEngine.UI;

public class BezierController : MonoBehaviour
{
    [SerializeField] private BeizerLine[] _bezierLines;
    [SerializeField] private Transform _mover;
    [SerializeField] private Slider _slider; 
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(delegate {UpdateLine ();});
    }

    private void OnEnable()
    {
        foreach (var bezierLine in _bezierLines)
        {
            bezierLine.Origin.OnNodeDragAction += OnNodeDragActionHandler;
            bezierLine.End.OnNodeDragAction += OnNodeDragActionHandler;
        }
    }

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void UpdateLine()
    {
        _lineRenderer.positionCount = (int)_slider.value + 1;
        
        foreach (var bezierLine in _bezierLines)
        {
            bezierLine.Interpolation = (int)_slider.value;
            bezierLine.UpdateLine();
        }

        _lineRenderer.SetPosition((int)_slider.value, _mover.transform.position); 
    
    }

    private void UpdateMainNodes()
    {
        foreach (var bezierLine in _bezierLines)
        {
            bezierLine.UpdateLine();
        }
    }

    private void OnNodeDragActionHandler()
    {
        UpdateMainNodes();
    }
}