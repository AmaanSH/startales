using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsScene : CoreScene
{
    public string consId;

    public override IEnumerator Play()
    {
        ConstellationManager.SetMode(Mode.Constellation);

        ConstellationManager.StartConsetllation(consId);

        yield return null;
    }
}
