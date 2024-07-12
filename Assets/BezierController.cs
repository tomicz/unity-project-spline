using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BezierController : MonoBehaviour
{
    [SerializeField] private BeizerLine[] _bezierLines;
    [SerializeField] private Transform _mover;
    [SerializeField] private Slider _slider; 

    private LineRenderer _lineRenderer;
    private List<Vector3> _lineVertices;

    private void Start()
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
        _lineRenderer.positionCount = 51;
        _lineRenderer.SetPositions(_bezierLines[2].Positions.ToArray());
        _bezierLines[2].DrawLine();
    }

    private void UpdateMovers()
    {
        int sliderValue = (int)_slider.value;

        foreach(var bezierLine in _bezierLines)
        {
            bezierLine.UpdateMover(sliderValue);
        }
    }
}