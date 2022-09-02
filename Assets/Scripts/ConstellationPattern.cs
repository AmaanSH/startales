using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationPattern : MonoBehaviour
{
    public List<ConstellationTile> points;
    public Sprite completedSprite;
    public SpriteRenderer spriteRenderer;

    public List<Speech> speechToLoad;

    private int completed = 0;
    private int speechIndex = 0;

    public void ConstellationPointCompleted()
    {
        completed += 1;

        if (completed == points.Count)
        {
            spriteRenderer.sprite = completedSprite;
            ConstellationManager.IncrementCompleted();
        }
    }
}
