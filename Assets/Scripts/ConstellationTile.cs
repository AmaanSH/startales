using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstellationTile : MonoBehaviour
{
    public ConstellationTile connecting;

    private bool active = true;
    private Image image;
    
    void Awake()
    {
        image = GetComponent<Image>();
    }

    public bool CanConnect(ConstellationTile tile)
    {
        return (tile == connecting) ? true : (tile.connecting == this) ? true : false;
    }
    
    public void OnClick()
    {
        Constellation.HandleSelected(this);
    }

    public void Select()
    {
        image.color = Color.red;
    }

    public void Deselect()
    {
        image.color = Color.white;
    }

}
