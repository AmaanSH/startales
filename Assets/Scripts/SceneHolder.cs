using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneHolder : MonoBehaviour
{
    [HideInInspector]
    public List<CoreScene> coreScenes;

    private void Awake()
    {
        coreScenes = GetComponentsInChildren<CoreScene>().ToList();
    }
}
