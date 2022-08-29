using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    public static ConstellationManager instance;
    public Transform plane;
    public LineRenderer lineRenderer;

    private ConstellationTile _selected;
    private LineRenderer _line;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void StartDrag(ConstellationTile tile, Vector3 point)
    {
        LineRenderer lr = Instantiate(instance.lineRenderer, tile.gameObject.transform);

        lr.positionCount += 2;
        lr.SetPosition(0, point);

        instance._selected = tile;
        instance._line = lr;
    }

    public static void Dragging()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = instance._selected.gameObject.transform.position.z;

        Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);
        instance._line.SetPosition(1, position);
    }

    public static void EndDrag()
    {
        int layerIndex = LayerMask.NameToLayer("Stars");
        int layerMask = (1 << layerIndex);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, layerMask))
        {
            ConstellationTile tile = hit.collider.GetComponent<ConstellationTile>();
            if (instance._selected.CanConnect(tile))
            {
                instance._line.SetPosition(1, hit.transform.position);
                instance._selected.RemoveConnection();
                tile.RemoveConnection();
            }
            else
            {
                instance._line.positionCount = 0;
                Destroy(instance._line.gameObject);
            }
        }
        else
        {
            instance._line.positionCount = 0;
            Destroy(instance._line.gameObject);
        }

        instance._selected = null;
        instance._line = null;
    }
}
