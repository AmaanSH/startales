using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum Placement
{
    FarLeft,
    Left,
    Right,
    FarRight
}


[System.Serializable]
public class Slot
{
    public Placement placement;
    public Image holder;

    [HideInInspector]
    public Character character;
}

public class CharacterPanel : MonoBehaviour
{
    public static CharacterPanel instance;

    public List<Slot> slots;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private Slot GetSlot(Placement placement)
    {
        return slots.Find(x => x.placement == placement);
    }

    private Slot GetSlot(Character character)
    {
        return slots.Find(x => x.character == character);
    }

    public void SetCharacterInSlot(Placement placement, Character character)
    {
        Slot slot = GetSlot(placement);
        Sprite characterSprite = Characters.GetCharacterSprite(character);

        // okay set the sprite to the character
        slot.holder.sprite = characterSprite;
        slot.character = character;
    }

    public void RemoveCharacterInSlot(Placement placement)
    {
        Slot slot = GetSlot(placement);

        slot.holder.sprite = null;
    }

    public void SetGreyscale()
    {
        foreach (Slot s in slots)
        {
            s.holder.material.SetFloat("_EffectAmount", 1);
        }
    }

    public void SetTalking(Character character)
    {
        Slot slot = GetSlot(character);

        // set the greyscale shader _EffectAmount to 0
        slot.holder.material.SetFloat("_EffectAmount", 0);

        // set the other slots shader _EffectAmount to 1
        foreach (Slot s in slots)
        {
            if (s != slot)
            {
                s.holder.material.SetFloat("_EffectAmount", 1);
            }
        }
    }
}
