using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Music
{
    public AudioClip clip;
    public string id;
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource source;
    public AudioMixer audioMixer;
    public string exposedParam;

    public List<Music> music;

    private float duration = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void PlayAudio(string id)
    {
        AudioClip clip = instance.GetAudio(id);
        if (clip)
        {
            if (instance.source.clip != clip)
            {
                if (!instance.source.isPlaying)
                {
                    instance.source.clip = clip;
                    instance.source.Play();

                    instance.StartCoroutine(instance.Fade(1.0f));
                }
                else
                {
                    instance.StartCoroutine(instance.Fade(0, clip));
                }
            }
        }
    }

    private AudioClip GetAudio(string id)
    {
        return instance.music.Find(x => x.id == id).clip;
    }

    private IEnumerator Fade(float targetVolume, AudioClip newClip = null)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }

        if (newClip != null)
        {
            instance.source.clip = newClip;
            instance.source.Play();
            instance.StartCoroutine(Fade(1.0f));
        }

        yield break;
    }
}
