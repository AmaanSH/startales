using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstellationTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ConstellationTile connecting;

    [HideInInspector]
    public bool active = true;

    [HideInInspector]
    public ConstellationPattern parent;

    private LineRenderer _lineRenderer;
    private int connections = 2;
    
    
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        parent = GetComponentInParent<ConstellationPattern>();
    }

    public LineRenderer GetLineRenderer()
    {
        return _lineRenderer;
    }

    public bool CanConnect(ConstellationTile tile)
    {
        return (tile == connecting) ? true : (tile.connecting == this) ? true : false;
    }
    
    public void OnBeginDrag(PointerEventData data)
    {
        if (active)
        {
            ConstellationManager.StartDrag(this, transform.position);
        }
    }

    public void RemoveConnection()
    {
        connections -= 1;

        if (connections <= 0)
        {
            active = false;

            parent.ConstellationPointCompleted();
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (active)
        {
            ConstellationManager.Dragging();
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (active)
        {
            ConstellationManager.EndDrag();
        }
    }
}
