using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour
{
    public static Constellation instance;

    private ConstellationTile _selected;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void HandleSelected(ConstellationTile selected)
    {
        if (instance)
        {
            if (instance._selected == null)
            {
                instance._selected = selected;
                selected.Select();

                Debug.LogFormat("Selected: {0}", selected.gameObject.name);

                // TODO: change the colour? 
                // TODO: draw the line bleh bleh
                // TODO: sound effects pog
            }
            else
            {
                // okay.. lets check to see if the one we selected can connect with the one we had previously selected
                bool correct = instance._selected.CanConnect(selected);
                Debug.LogFormat("{0} connecting to {1} - {2}", instance._selected.gameObject.name, selected.gameObject.name, correct);

                instance._selected.Deselect();

                instance._selected = null;

            }
        }
    }
}
