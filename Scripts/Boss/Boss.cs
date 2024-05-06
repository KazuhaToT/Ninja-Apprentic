using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    protected enum State
    {
        FreeRoam,
    }
    protected State state;
    protected BossPathfinding pathfinding;
    [SerializeField] protected PlayerController player;
    [SerializeField] protected Slider hpSlider;
    [SerializeField] protected TMP_Text hpText;

    [SerializeField] public float maxHealth = 1000;
    [SerializeField] public float attack = 20;
    [SerializeField] public GameObject skill_1_prefab;
    [SerializeField] public GameObject skill_3_prefab;
    [SerializeField] public GameObject warningCircle;
    [SerializeField] public GameObject transformDungeon;
    [SerializeField] public bool spawner;


    public float health;
    public int expAmount = 500;
    public bool checkDie = false;
    public Animator animator;
    public bool isSkill = false;
    public bool skill_2 = false;

    public Vector3 initialPosition;

    private void Update()
    {
        hpSlider.maxValue = maxHealth;
        hpSlider.value = health;
        hpText.text = "HP: " + health + "/" + maxHealth;
        if (pathfinding.isChasing)
        {
            animator.SetBool("isMove", true);
        }
    }
    private void OnEnable()
    {
        pathfinding = GetComponent<BossPathfinding>();
        animator = GetComponent<Animator>();
        state = State.FreeRoam;
        skill_1_prefab.SetActive(false);
        skill_3_prefab.SetActive(false);
        warningCircle.SetActive(false);
        initialPosition = transform.position;
        InvokeRepeating("RandomSkill", 3f, 3f);
    }
    public void SetHP()
    {
        animator.SetBool("isMove", false);
        health = maxHealth;
    }
    public void UnderAttack(float damage)
    {
        animator.SetBool("isHurt", true);
        health -= damage;
        StartCoroutine(OffAnimation());
        Die();
    }
    protected IEnumerator OffAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isHurt", false);
    }
    protected void Die()
    {
        if (health <= 0)
        {
            checkDie = true;
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            ExpManager.Instance.AddExp(expAmount);
            if (spawner)
            {
                EnemySpawner.Instance.AddEnemyToRespawnQueue(transform.gameObject);
            }
            else
            {
                transformDungeon.SetActive(true);
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }
    public void RandomSkill()
    {
        if (pathfinding.isChasing && !checkDie)
        {
            int randomSkill = Random.Range(1, 4);
            switch (randomSkill)
            {
                case 1:
                    Skill_1();
                    break;
                case 2:
                    Skill_2();
                    break;
                case 3:
                    Skill_3();
                    break;
            }
        }
    }

    public void Skill_1()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            StartCoroutine(Skill_1_Routine());
        }
    }

    private IEnumerator Skill_1_Routine()
    {
        isSkill = true;
        animator.SetBool("isAttack", true);
        warningCircle.transform.position = transform.position;
        warningCircle.SetActive(true);
        warningCircle.transform.localScale = new Vector3(5f, 5f, 1f);
        yield return new WaitForSeconds(1f);
        warningCircle.SetActive(false);
        skill_1_prefab.SetActive(true);
        skill_1_prefab.transform.position = transform.position;
        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            player.UnderAttack(attack);
        }
        yield return new WaitForSeconds(1f);
        skill_1_prefab.SetActive(false);
        isSkill = false;
        animator.SetBool("isAttack", false);
    }
    public void Skill_2()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 7f)
        {
            StartCoroutine(Skill_2_Routine());
        }
    }

    private IEnumerator Skill_2_Routine()
    {
        skill_2 = true;
        isSkill = true;
        animator.SetBool("isAttack", true);
        warningCircle.transform.position = transform.position;
        warningCircle.SetActive(true);
        warningCircle.transform.localScale = new Vector3(16f, 16f, 1f);
        yield return new WaitForSeconds(1.5f);
        warningCircle.SetActive(false);
        isSkill = false;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 7f);

        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            player.UnderAttack(attack);
        }
        skill_2 = false;
        animator.SetBool("isAttack", false);
    }
    public void Skill_3()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 7f)
        {
            Vector3 playerPos = player.transform.position;
            StartCoroutine(Skill_3_Routine(playerPos));
        }
    }

    private IEnumerator Skill_3_Routine(Vector3 playerPos)
    {
        isSkill = true;
        animator.SetBool("isAttack", true);
        warningCircle.transform.position = playerPos;
        warningCircle.SetActive(true);
        warningCircle.transform.localScale = new Vector3(4f, 4f, 1f);
        yield return new WaitForSeconds(1f);
        warningCircle.SetActive(false);
        skill_3_prefab.SetActive(true);
        skill_3_prefab.transform.position = playerPos;
        if (Vector3.Distance(playerPos, player.transform.position) < 2f)
        {
            player.UnderAttack(attack);
        }
        yield return new WaitForSeconds(1f);
        skill_3_prefab.SetActive(false);
        isSkill = false;
        animator.SetBool("isAttack", false);
    }
}
