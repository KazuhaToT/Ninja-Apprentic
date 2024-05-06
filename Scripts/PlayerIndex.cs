using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerIndex : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerController playerController;

    [SerializeField] protected TMP_Text text;

    protected void OnEnable()
    {
        text.text = "Level : " + playerController.currentLevel.ToString() +
                    "\nBase Attack : " + player.baseAttack.ToString() +
                    "\nAttack : " + (player.attack + 100).ToString() + "%" +
                    "\nBase HP : " + playerController.maxHealth.ToString() +
                    "\nSpeed Attack : " + (player.speedAttack + 100).ToString() + "%" +
                    "\nCrit Rate : " + player.critRate.ToString() + "%" +
                    "\nCrit Damage : " + player.critDamage.ToString() + "%" +
                    "\nBreak Amount : " + player.breakAmount.ToString() + "%" ;
    }
}
