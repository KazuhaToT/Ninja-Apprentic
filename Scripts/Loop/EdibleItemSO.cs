using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[CreateAssetMenu]
public class EdibleItemSO : LootManager, IDestroyableItem, IItemAction
{
    [SerializeField] protected List<ModifierData> modifiersData = new List<ModifierData>();
    public string ActionName => "Consume";
    [field: SerializeField]
    public AudioClip actionSFX { get; private set; }

    public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        foreach (var modifierData in modifiersData)
        {
            modifierData.statModifier.AffectCharacter(character, modifierData.value);
        }
        return true;
    }
}
public interface IDestroyableItem
{

}
public interface IItemAction
{
    public string ActionName { get; }
    public AudioClip actionSFX { get; }
    bool PerformAction(GameObject character, List<ItemParameter> itemState = null);
}
[System.Serializable]
public class ModifierData{
    public CharacterStatModifiersSO statModifier;
    public float value;
}
