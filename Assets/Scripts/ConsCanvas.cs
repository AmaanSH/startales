using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsCanvas : MonoBehaviour
{
    public TextMeshProUGUI remaining;

    public void UpdateReamining(int total, int current)
    {
        if (total == current)
        {
            remaining.text = "All constallations uncovered, continue with the story";
        }
        else
        {
            remaining.text = string.Format("Remaining {0}/{1}", current, total);
        }
    }

}
