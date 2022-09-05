using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = AudioListener.volume;
    }

    public void Show()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
