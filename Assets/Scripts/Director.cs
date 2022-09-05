using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        LoadScene(0);
    }

    public static void LoadScene(int index)
    {
        if (index >= instance.scenes.Count)
        {
            // okay load the credit scene pog
            SceneManager.LoadScene("Credits");
        }
        else
        {
            if (instance)
            {
                Debug.LogFormat("Starting scene {0}/{1}", index + 1, instance.scenes.Count);

                instance.currentScenes = instance.scenes[index].coreScenes;
                instance.currentSceneIndex = index;
                instance.currentIndex = 0;

                instance.StartCoroutine(Play());
            }
        }
    }

    public static IEnumerator Play()
    {
        Debug.LogFormat("Playing {0}/{1}", instance.currentIndex + 1, instance.currentScenes.Count);

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
                LoadScene(instance.currentSceneIndex + 1);
            }
            else
            {
                instance.StartCoroutine(Play());
            }
        }
    }
}
