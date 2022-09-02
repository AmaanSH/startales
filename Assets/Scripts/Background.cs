using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public static Background instance;
    public Image holder;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void Show(bool show)
    {
        instance.holder.gameObject.SetActive(show);
        CharacterPanel.instance.Show(show);
    }

    public static void SetImage(Sprite sprite)
    {
        instance.holder.sprite = sprite;

        instance.holder.gameObject.SetActive(true);
    }
}
