using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlayer : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] public float buffDame = 1f;
    [SerializeField] protected float multipledame = 1f;
    [SerializeField] public float timeDelete = 0.2f;
    [SerializeField] public float timeD = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyAI>().UnderAttack(multipledame * player.damage);
            StartCoroutine(DestroySkill());
        }
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().UnderAttack(multipledame * player.damage);
            Debug.Log(multipledame + " + " + player.damage);
            StartCoroutine(DestroySkill());
        }
    }
    public IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(timeD);
        player.baseAttack /= buffDame;
        gameObject.SetActive(false);
    }
}
