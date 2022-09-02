using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.WSA;
using System;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speechText;
    public Image characterImage;

    private bool finished;
    private bool skip;

    private bool constellationMode;

    private List<Speech> speeches = new List<Speech>();
    private int currentIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator AutoSkip()
    {
        if (currentIndex < speeches.Count - 1)
        {
            yield return new WaitForSeconds(1.0f);

            currentIndex++;

            Speech current = speeches[currentIndex];
            StartCoroutine(SetText(current.text, current.character, true));
        }
        else
        {
            currentIndex = 0;
            speeches = new List<Speech>();

            gameObject.SetActive(false);
        }
    }

    public void LoadDialogues(List<DialogueScene> diagloueScenes)
    {
        speeches = new List<Speech>();

        for (int i = 0; i < diagloueScenes.Count; i++)
        {
            speeches.Add(diagloueScenes[i].speech);
        }

        currentIndex = 0;
        Speech speech = speeches[0];
        
        gameObject.SetActive(true);

        StartCoroutine(SetText(speech.text, speech.character, true));
    }

    public IEnumerator SetText(string text, Character character, bool characterInSpeech)
    {
        gameObject.SetActive(true);

        if (character == Character.None)
        {
            nameText.transform.parent.gameObject.SetActive(false);
            
            CharacterPanel.instance.SetGreyscale();
        }
        else
        {
            nameText.transform.parent.gameObject.SetActive(true);
            
            CharacterPanel.instance.SetTalking(character);
        }

        string name = Characters.GetCharacterName(character);
        
        if (characterInSpeech)
        {
            constellationMode = true;
            
            CharacterPanel.instance.Show(false);
            Sprite sprite = Characters.GetCharacterSprite(character);

            characterImage.sprite = sprite;
            characterImage.gameObject.SetActive(true);  
        }
        else
        {
            CharacterPanel.instance.Show(true);
            characterImage.gameObject.SetActive(false);

            constellationMode = false;
        }

        finished = false;
        skip = false;

        speechText.text = text;
        nameText.text = name;

        //sfxForTyping.Play();

        yield return null;

        int counter = 0;
        int totalVisibleCharacters = speechText.textInfo.characterCount;

        while (counter <= totalVisibleCharacters && !skip)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            speechText.maxVisibleCharacters = visibleCount;

            counter += 1;

            yield return new WaitForSeconds(0.05f);

            if (visibleCount < totalVisibleCharacters)
            {
                int current = visibleCount;
                TMP_CharacterInfo characterInfo = speechText.textInfo.characterInfo[(current > 0) ? current - 1 : current];
                if (char.IsPunctuation(characterInfo.character) && (characterInfo.character != '�' && characterInfo.character.ToString() != "'" && characterInfo.character != '(' && characterInfo.character != ')'))
                {
                    //sfxForTyping.Stop();
                    yield return new WaitForSeconds(0.3f);
                    //sfxForTyping.Play();
                }
            }
        }

        //sfxForTyping.Stop();

        if (speechText.maxVisibleCharacters == totalVisibleCharacters)
        {
            finished = true;
        }

        if (skip)
        {
            speechText.maxVisibleCharacters = totalVisibleCharacters;
        }

        if (constellationMode)
        {
            StartCoroutine(AutoSkip());
        }
    }

    public void NextOption()
    {
        if (!constellationMode)
        {
            if (!skip && !finished)
            {
                skip = true;
            }
            else
            {
                gameObject.SetActive(false);

                Director.Next();
            }
        }
    }
}