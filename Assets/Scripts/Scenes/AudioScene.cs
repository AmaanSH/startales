using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScene : CoreScene
{
    public string id;
    public override IEnumerator Play()
    {
        MusicManager.PlayAudio(id);

        Director.Next();

        yield return null;
    }
}
