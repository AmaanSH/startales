using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsControllerScene : CoreScene
{
    public bool disableInput;
    public bool cleanup;

    public override IEnumerator Play()
    {
        Director.Next();

        yield return null;
    }
}
