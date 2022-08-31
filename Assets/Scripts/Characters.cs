using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterImage
{
    public Character character;
    public Sprite sprite;
}

public enum Character
{
    Singer,
    Keyboard,
    Guitarist,
    Drummer,
    None
}

public class Characters : MonoBehaviour
{
    public static Characters instance;

    public List<CharacterImage> characters;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static Sprite GetCharacterSprite(Character character)
    {
        return instance.characters.Find(x => x.character == character).sprite;
    }

    public static string GetCharacterName(Character character)
    {
        switch (character)
        {
            case Character.Guitarist:
                return "Guitarist";
            case Character.Singer:
                return "Singer";
            case Character.Keyboard:
                return "Keyboard";
            case Character.Drummer:
                return "Drummer";
        }

        return "";
    }
}
