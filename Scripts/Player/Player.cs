using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] public float baseAttack = 10;
    [SerializeField] public float attack = 0;
    [SerializeField] public float speedAttack;
    [SerializeField] public float critRate = 5;
    [SerializeField] public float critDamage = 150;
    [SerializeField] public float breakAmount;
    [SerializeField] public SceneInfo sceneInfo;

    public float damage;
    private void Update()
    {
        CritDamage();
    }
    protected void CritDamage()
    {
        int randoom = Random.Range(0, 100);
        if (randoom <= critRate)
        {
            damage = baseAttack * ((100 + attack) / 100) * (critDamage / 100);
        }
        else
        {
            damage = baseAttack * ((100 + attack) / 100);
        }
    }
}
