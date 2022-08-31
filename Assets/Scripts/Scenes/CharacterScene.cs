using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterPlacement
{
    public Placement placement;
    public Character character;
}

public class CharacterScene : CoreScene
{
    public List<CharacterPlacement> characterPlacements;

    public override IEnumerator Play()
    {
        // iterate through character placement, put the people in the right slot
        foreach(CharacterPlacement placement in characterPlacements)
        {
            CharacterPanel.instance.SetCharacterInSlot(placement.placement, placement.character);
        }

        Director.Next();

        yield return null;
    }
}
