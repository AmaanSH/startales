using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Mode
{
    VN,
    Constellation
}

public class ConstellationManager : MonoBehaviour
{
    public static ConstellationManager instance;
    public Transform plane;
    public LineRenderer lineRenderer;
    public ConsCanvas consCanvas;

    public List<ConsHolder> cons;

    public float cameraSpeed = 2f;

    public Image blocker;

    [HideInInspector]
    public bool consCompleted;

    private ConstellationTile _selected;
    private LineRenderer _line;

    private ConsHolder currentHolder;
    private int totalConstellations;
    private int completedCount;

    private Mode mode;

    private Vector3 defaultPositionCamera;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        defaultPositionCamera = Camera.main.transform.position;
    }

    public static Mode GetMode()
    {
        return instance.mode;
    }

    public static void SetMode(Mode md)
    {
        instance.mode = md;
    }

    public static void SetBlocker(bool active)
    {
        instance.blocker.gameObject.SetActive(active);
    }

    private void Update()
    {
        // check if mouse is in focus of application
        if (Application.isFocused && mode == Mode.Constellation)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 viewportMousePosition = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);

            // check if mouse is in the outermost 10% of the screen
            if (viewportMousePosition.x < 0.1f || viewportMousePosition.x > 0.9f || viewportMousePosition.y < 0.1f || viewportMousePosition.y > 0.9f)
            {
                if (mousePosition.x <= 0)
                {
                    MoveCamera("left");
                }
                else if (mousePosition.x >= Screen.width - 1)
                {
                    MoveCamera("right");
                }

                if (mousePosition.y <= 0)
                {
                    MoveCamera("down");
                }
                else if (mousePosition.y >= Screen.height - 1)
                {
                    MoveCamera("up");
                }
            }
        }
    }
    
    private void MoveCamera(string direction)
    {
        switch (direction)
        {
            case "right":
                Camera.main.transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, 0, 0));
                break;
            case "left":
                Camera.main.transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, 0, 0));
                break;
            case "down":
                Camera.main.transform.Translate(new Vector3(0, -cameraSpeed * Time.deltaTime, 0));
                break;
            case "up":
                Camera.main.transform.Translate(new Vector3(0, cameraSpeed * Time.deltaTime, 0));
                break;
        }

        // clamp camera so it does not move past the top or bottom or left or right of the plane
        float clampNumber = 1.5F;
        Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, plane.position.x - clampNumber, plane.position.x + clampNumber), Mathf.Clamp(Camera.main.transform.position.y, plane.position.y - clampNumber, plane.position.y + clampNumber), Camera.main.transform.position.z);
    }

    public static void StartConsetllation(string name, bool playMusic)
    {
        ConsHolder holder = instance.cons.Find(x => x.id == name);
        if (holder)
        {
            instance.consCompleted = false;
            instance.currentHolder = holder;
            instance.totalConstellations = holder.consPatterns.Count;

            Background.Show(false);

            instance.consCanvas.UpdateReamining(instance.totalConstellations, instance.completedCount);

            holder.gameObject.SetActive(true);
            instance.consCanvas.gameObject.SetActive(true);

            if (playMusic)
            {
                MusicManager.PlayAudio("reflection");
            }

            //Camera.main.transform.position = instance.defaultPositionCamera;
        }
    }

    public static void IncrementCompleted()
    {
        instance.completedCount += 1;

        instance.consCanvas.UpdateReamining(instance.totalConstellations, instance.completedCount);

        if (instance.completedCount == instance.totalConstellations)
        {
            instance.consCompleted = true;

            // only continue if the story segement is over
            if (Dialogue.instance.storyCompleted)
            {
                SetMode(Mode.VN);

                Dialogue.instance.StopAllCoroutines();
                Director.Next();

                instance.currentHolder.gameObject.SetActive(false);
                instance.currentHolder = null;
                instance.totalConstellations = 0;
                instance.completedCount = 0;
                
                CharacterPanel.instance.Show(true);
                instance.consCanvas.gameObject.SetActive(false);
            }
        }
    }

    public static void Cleanup()
    {
        SetMode(Mode.VN);

        instance.currentHolder.gameObject.SetActive(false);
        CharacterPanel.instance.Show(true);

        instance.currentHolder = null;
        instance.totalConstellations = 0;
        instance.completedCount = 0;

        instance.consCanvas.gameObject.SetActive(false);

        Director.Next();
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
