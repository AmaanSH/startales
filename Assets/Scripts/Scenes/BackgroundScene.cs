using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScene : CoreScene
{
    public Sprite sprite;

    public override IEnumerator Play()
    {
        Background.SetImage(sprite);

        Director.Next();

        yield return null;
    }
}
