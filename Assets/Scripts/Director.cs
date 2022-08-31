using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    public static Director instance;

    public List<SceneHolder> scenes;

    private List<CoreScene> currentScenes;

    private int currentSceneIndex = 0;
    private int currentIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void LoadScene(int index)
    {
        if (instance)
        {
            instance.currentScenes = instance.scenes[index].coreScenes;
            instance.currentSceneIndex = index;
            instance.currentIndex = 0;

            instance.StartCoroutine(Play());
        }
    }

    public static IEnumerator Play()
    {
        CoreScene scene = instance.currentScenes[instance.currentIndex];

        yield return instance.StartCoroutine(scene.Play());
    }

    public static void Next()
    {
        if (instance)
        {
            instance.currentIndex += 1;
           
            if (instance.currentIndex >= instance.currentScenes.Count)
            {
                // switch...
            }
            else
            {
                instance.StartCoroutine(Play());
            }
        }
    }
}
