using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class LootManager : ScriptableObject
{
    public Sprite lootSprite;
    public string lootName;
    public int dropChance;
    public int price;
    public int ID => GetInstanceID();
    public int maxSizeStack = 1;
    public bool isStackable;
    [field: TextArea]
    public string Description;
    [field: SerializeField]
    public List<ItemParameter> DefaultParametersList { get; set; }

}
[Serializable]
public struct ItemParameter : IEquatable<ItemParameter>
{
    public ItemParameterSO itemParameter;
    public float value;

    public bool Equals(ItemParameter other)
    {
        return other.itemParameter == itemParameter;
    }
}
