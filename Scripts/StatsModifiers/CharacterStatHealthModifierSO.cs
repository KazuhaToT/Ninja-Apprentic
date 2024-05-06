using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifiersSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerController playerController = character.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.AddHealth((int)val);
        }
    }
}
