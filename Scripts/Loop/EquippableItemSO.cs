using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquippableItemSO : LootManager, IDestroyableItem, IItemAction
{
    public string ActionName => "Equip";

    [field: SerializeField] public int minBaseAttack;
    [field: SerializeField] public int maxBaseAttack;
    [field: SerializeField] public int minAttack;
    [field: SerializeField] public int maxAttack;
    [field: SerializeField] public int minSpeedAttack;
    [field: SerializeField] public int maxSpeedAttack;
    [field: SerializeField] public int minCritRate;
    [field: SerializeField] public int maxCritRate;
    [field: SerializeField] public int minCritDamage;
    [field: SerializeField] public int maxCritDamage;
    [field: SerializeField] public int minBreakAmount;
    [field: SerializeField] public int maxBreakAmount;


    public int baseAttack { get; private set; }
    public int attack { get; private set; }
    public int speedAttack { get; private set; }
    public int critRate { get; private set; }
    public int critDamage { get; private set; }
    public int breakAmount { get; private set; }


    [field: SerializeField] public AudioClip actionSFX { get; private set; }

    public void Initialize()
    {
        baseAttack = Random.Range(minBaseAttack, maxBaseAttack);
        attack = Random.Range(minAttack, maxAttack);
        speedAttack = Random.Range(minSpeedAttack, maxSpeedAttack);
        critRate = Random.Range(minCritRate, maxCritRate);
        critDamage = Random.Range(minCritDamage, maxCritDamage);
        breakAmount = Random.Range(minBreakAmount, maxBreakAmount);
        Description = "Base Attack: " + baseAttack +
            "\nAttack: " + attack + "%" +
            "\nSpeed Attack: " + speedAttack + "%" +
            "\nCrit Rate: " + critRate + "%" +
            "\nCrit Damage: " + critDamage + "%" +
            "\nBreak Amount: " + breakAmount + "%";
    }

    public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
        if (weaponSystem != null)
        {
            weaponSystem.SetWeapon(this, itemState == null ?
                DefaultParametersList : itemState);
            return true;
        }
        return false;
    }
}