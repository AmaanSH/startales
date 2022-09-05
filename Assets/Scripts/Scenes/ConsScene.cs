using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConsScene : CoreScene
{
    public string consId;
    public bool playPuzzleMusic;
    public GameObject speechHolder;

    public override IEnumerator Play()
    {
        ConstellationManager.SetMode(Mode.Constellation);

        List<DialogueScene> dialogueScenes = speechHolder.GetComponentsInChildren<DialogueScene>().ToList();

        yield return StartCoroutine(ConstellationManager.StartConsetllation(consId, playPuzzleMusic, dialogueScenes));

        yield return null;
    }
}
