using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationPattern : MonoBehaviour
{
    public List<ConstellationTile> points;
    public Sprite completedSprite;
    public SpriteRenderer spriteRenderer;

    private int completed = 0;

    public void ConstellationPointCompleted()
    {
        completed += 1;

        if (completed == points.Count)
        {
            spriteRenderer.sprite = completedSprite;
        }

        // TODO dialogue
        // TODO effect?

    }
}
