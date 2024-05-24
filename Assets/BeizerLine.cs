using System.Collections.Generic;
using UnityEngine;

public class BeizerLine : MonoBehaviour
{
    public SceneNode Origin => _origin;
    public SceneNode End => _end;
    [SerializeField] private SceneNode _origin;
    [SerializeField] private SceneNode _end;
    [SerializeField] private Transform _mover;

    [Range(0,50)]
    public int Interpolation;
    public int NodeCount = 10;
    public List<Transform> Nodes = new List<Transform>();


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
            Nodes.Add(newNode);
        }
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

    public void UpdateLine()
    {
        Vector3 distance = _end.transform.position - _origin.transform.position;
        Vector3 fraction = distance/NodeCount;

        for (int i = 0; i <= NodeCount; i++)
        {
            Nodes[i].position = _origin.transform.position + fraction * i;
        }
        
        _mover.position = Nodes[Interpolation].position;
    }

    private void OnNodeDragActionHandler()
    {
        UpdateLine();
    }
}