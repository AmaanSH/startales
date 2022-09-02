using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerScene : CoreScene
{
    public bool blocker;

    public override IEnumerator Play()
    {
        ConstellationManager.SetBlocker(blocker);

        Director.Next();

        yield return null;
    }
}
