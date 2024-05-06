using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }
        DisModifyParameters();
        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        Player player = gameObject.GetComponent<Player>();

        player.baseAttack += weapon.baseAttack;
        player.attack += weapon.attack;
        player.speedAttack += weapon.speedAttack;
        player.critRate += weapon.critRate;
        player.critDamage += weapon.critDamage;
        player.breakAmount += weapon.breakAmount;
        // foreach (var parameter in parametersToModify)
        // {
        //     if (itemCurrentState.Contains(parameter))
        //     {
        //         int index = itemCurrentState.IndexOf(parameter);
        //         float newValue = itemCurrentState[index].value + parameter.value;
        //         itemCurrentState[index] = new ItemParameter
        //         {
        //             itemParameter = parameter.itemParameter,
        //             value = newValue
        //         };
        //     }
        // }
    }
    protected void DisModifyParameters(){
        if(weapon == null) return;
        Player player = gameObject.GetComponent<Player>();

        player.baseAttack -= weapon.baseAttack;
        player.attack -= weapon.attack;
        player.speedAttack -= weapon.speedAttack;
        player.critRate -= weapon.critRate;
        player.critDamage -= weapon.critDamage;
        player.breakAmount -= weapon.breakAmount;
    }
}
