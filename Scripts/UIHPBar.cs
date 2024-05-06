using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected Slider hpSlider;
    [SerializeField] protected Text hpText;
    [SerializeField] protected Slider expSlider;
    [SerializeField] protected TMP_Text levelText;


    private void Update()
    {
        hpSlider.maxValue = player.GetComponent<PlayerController>().MaxHealth();
        hpSlider.value = player.GetComponent<PlayerController>().getHealth;
        expSlider.maxValue = player.GetComponent<PlayerController>().MaxExp();
        expSlider.value = player.GetComponent<PlayerController>().CurrentExp();
        hpText.text = "HP: " + player.GetComponent<PlayerController>().getHealth + "/" + player.GetComponent<PlayerController>().MaxHealth();
        levelText.text =player.GetComponent<PlayerController>().currentLevel.ToString();
    }
}
