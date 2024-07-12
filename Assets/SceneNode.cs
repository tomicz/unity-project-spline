using System;
using UnityEngine;

public class SceneNode : MonoBehaviour
{
    public event Action OnNodeDragAction;

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = mouseWorldPosition;
        OnNodeDragAction?.Invoke();
    }
}