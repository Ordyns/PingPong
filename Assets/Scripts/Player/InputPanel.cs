using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputPanel : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event System.Action DragStarted;
    public event System.Action<Vector2> Dragging;

    public bool IsDragging { get; private set; }
    public Vector2 Delta { get; private set; }

    private Vector2 _dragStartPosition;
    
    public void OnBeginDrag(PointerEventData eventData){
        _dragStartPosition = eventData.position;
        IsDragging = true;

        DragStarted?.Invoke();
    }

    public void OnDrag(PointerEventData eventData){
        float xDelta = eventData.position.x - _dragStartPosition.x;
        float yDelta = eventData.position.y - _dragStartPosition.y;
        Delta = new Vector2(xDelta, yDelta);
        
        Dragging?.Invoke(Delta);
    }

    public void OnEndDrag(PointerEventData eventData){
        IsDragging = false;
    }
}
