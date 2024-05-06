using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class EnemyAI : Health
{
    protected enum State
    {
        FreeRoam,
    }
    protected State state;
    protected EnemyPathfinding pathfinding;
    [SerializeField] protected float multiplesHealth = 1f;
    public float MaxHealth { get { return maxHealth; } }
    public float MultiplesHealth { get { return multiplesHealth; } }
    protected float damage = 4;
    [SerializeField] protected float multiplesDamage = 1f;
    protected float dame;
    public float Dame { get { return dame; } }
    [SerializeField] protected PlayerController player;
    [SerializeField] protected float speedAttack = 100;
    [SerializeField] protected Slider hpSlider;
    [SerializeField] protected TMP_Text hpText;
    [SerializeField] public bool spawner;


    public int expAmount = 50;
    public bool checkDie = false;
    public Vector3 initialPosition;
    private void OnEnable()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = State.FreeRoam;
        StartCoroutine(RoamingRoutine());
        dame = damage * multiplesDamage;
        initialPosition = transform.position;
    }
    private void Update()
    {
        hpSlider.maxValue = maxHealth;
        hpSlider.value = health;
        hpText.text = "HP: " + health + "/" + maxHealth;
    }
    protected IEnumerator RoamingRoutine()
    {
        while (state == State.FreeRoam)
        {
            Vector2 roamingPosition = GetRoamingPosition();
            pathfinding.MoveTo(roamingPosition);
            yield return new WaitForSeconds(1f);
        }
    }

    protected Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    public void SetHP()
    {
        health = maxHealth * multiplesHealth;
    }
    public void UnderAttack(float damage)
    {
        health -= damage;
        Die();
    }
    protected void Die()
    {
        if (health <= 0)
        {
            pathfinding.isAttack = false;
            checkDie = true;
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            ExpManager.Instance.AddExp(expAmount);
            if (spawner)
            {
                EnemySpawner.Instance.AddEnemyToRespawnQueue(transform.gameObject);
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!checkDie)
        {
            StartCoroutine(AttackPlayer(collision));
        }
    }

    IEnumerator AttackPlayer(Collision2D collision)
    {
        yield return new WaitForSeconds(100f / speedAttack);

        if (pathfinding.isAttack && !player.hurtPlayer)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().UnderAttack(dame);
            }
        }
    }
}

