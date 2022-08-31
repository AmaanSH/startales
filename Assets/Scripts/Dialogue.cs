using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.WSA;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speechText;

    private bool finished;
    private bool skip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameObject.SetActive(false);
        }
    }

    public IEnumerator SetText(string text, Character character)
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

        // TODO: make non talking characters grayscale

        string name = Characters.GetCharacterName(character);

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
                if (char.IsPunctuation(characterInfo.character) && (characterInfo.character != '’' && characterInfo.character.ToString() != "'" && characterInfo.character != '(' && characterInfo.character != ')'))
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
    }

    public void NextOption()
    {
        if (!skip && !finished)
        {
            skip = true;
        }
        else
        {
            gameObject.SetActive(false);

            // load next dialogue
            Director.Next();
        }
    }
}
