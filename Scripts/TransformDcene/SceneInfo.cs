using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "SceneInfo")]
public class SceneInfo : ScriptableObject
{
    public DataPlayer dataPlayer;

    public void SaveData(Player player, PlayerController playerController){
        dataPlayer.baseAttack = player.baseAttack;
        dataPlayer.attack = player.attack;
        dataPlayer.speedAttack = player.speedAttack;
        dataPlayer.critRate = player.critRate;
        dataPlayer.critDamage = player.critDamage;
        dataPlayer.breakAmount = player.breakAmount;
        dataPlayer.maxHealth = playerController.MaxHealth();
        dataPlayer.health = playerController.getHealth;
        dataPlayer.currentExp = playerController.currentExp;
        dataPlayer.maxExp = playerController.maxExp;
        dataPlayer.currentLevel = playerController.currentLevel;
    }
    
}
[System.Serializable]
public struct DataPlayer{
    public float baseAttack;
    public float attack;
    public float speedAttack;
    public float critRate;
    public float critDamage;
    public float breakAmount;
    public float maxHealth;
    public float health;
    public int currentExp;
    public int maxExp;
    public int currentLevel;
};
