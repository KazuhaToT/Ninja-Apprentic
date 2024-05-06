using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] protected float health;
    [SerializeField] protected float hp = 100f;

    public float getHealth
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

}
