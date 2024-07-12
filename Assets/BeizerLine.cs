using System.Collections.Generic;
using UnityEngine;

public class BeizerLine : MonoBehaviour
{
    public List<Vector3> Positions; 
    public SceneNode Origin => _origin;
    public SceneNode End => _end;
    public int NodeCount = 10;

    [SerializeField] private SceneNode _origin;
    [SerializeField] private SceneNode _end;
    [SerializeField] private Transform _mover;

    private List<Transform> _nodes = new List<Transform>();

    private void Awake()
    {
        _mover.position = _origin.transform.position;
        
        Vector3 distance = _end.transform.position - _origin.transform.position;
        Vector3 fraction = distance/NodeCount;

        for (int i = 0; i <= NodeCount; i++)
        {
            Transform newNode = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            newNode.localScale = Vector3.one * .25f;
            newNode.position = _origin.transform.position + fraction * i;
            _nodes.Add(newNode);
        }
    }

    private void Start()
    {
        DrawLine();
    }

    private void OnEnable()
    {
        _origin.OnNodeDragAction += OnNodeDragActionHandler;
        _end.OnNodeDragAction += OnNodeDragActionHandler;
    }

    private void OnDisable()
    {
        _origin.OnNodeDragAction -= OnNodeDragActionHandler;
        _end.OnNodeDragAction -= OnNodeDragActionHandler;
    }

    public void DrawLine()
    {
        Positions = new List<Vector3>();
        Vector3 distance = _end.transform.position - _origin.transform.position;
        Vector3 fraction = distance/NodeCount;          
        
        for (int i = 0; i <= NodeCount; i++)
        {
            Vector3 position = _origin.transform.position + fraction * i;

            _mover.position = position;
            _nodes[i].position = position;
            Positions.Add(_mover.position);
        }
    }

    public void UpdateMover(int index)
    {
        for(int i = 0; i < Positions.Count; i++)
        {
            _mover.transform.position = Positions[index];
        }
    }

    private void OnNodeDragActionHandler()
    {
        DrawLine();
    }

}