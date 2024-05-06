using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : Health
{
    protected float speed = 5.0f;
    protected bool isMoving;
    protected Vector2 input;
    protected Animator animator;
    protected Transform attackController;
    protected Animator attackAnimator;

    protected SpriteRenderer attackRenderer;
    Vector2 weapon = new Vector2(0, -1);
    public Vector2 Weapon { get { return weapon; } }
    [SerializeField] protected LayerMask NPC;
    [SerializeField] protected float multiplesDamege = 1f;
    [SerializeField] protected float timeHurt = 0.5f;
    [SerializeField] public int currentExp, maxExp, currentLevel;
    public int CurrentExp() { return currentExp; }
    public int MaxExp() { return maxExp; }
    int check = 0;
    public bool hurtPlayer = false;
    protected float hurt;
    public bool isSkill = false;
    [SerializeField] protected SceneInfo sceneInfo;

    protected float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = 5.5f, dashCooldown = 2f;

    public float dashCounter;
    public float dashCoolCounter;
    [SerializeField] public Vector2 lastMove;
    [SerializeField] public AudioSource hurtSourceF;
    [SerializeField] public AudioClip hurtSound;
    [SerializeField] public AudioSource dieSound;
    [SerializeField] public Vector2 playerLocation;
    [SerializeField] public GameObject buttonOut;
    [SerializeField] public GameObject outDungeon;
    private void Start()
    {
        animator = GetComponent<Animator>();
        attackController = transform.Find("SpriteInHand");
        attackAnimator = attackController.GetComponent<Animator>();
        attackController.gameObject.SetActive(false);
        health = maxHealth;
        gameObject.GetComponent<PickUpSystem>().inventory.gold = 0;
        activeMoveSpeed = speed;
        lastMove = transform.position;
    }

    protected void OnEnable()
    {
        ExpManager.Instance.OnExpChange += HandleExpChange;
    }
    protected void OnDisable()
    {
        ExpManager.Instance.OnExpChange -= HandleExpChange;
    }

    public void HandleUpdate()
    {
        playerLocation = transform.position;
        if (health == 0)
        {
            GameOver.Instance.SetOnShowDialog();
            Invoke("Die", 5f);
        }
        if (Input.GetMouseButton(0) && check == 0 && !isSkill)
        {
            check = 1;
            attackController.gameObject.SetActive(true);
            Invoke("StopAttack", 0.2f);
        }
        if (true)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                weapon = input;
            }

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                if (input.x != 0 && input.y != 0)
                {
                    transform.Translate(move * activeMoveSpeed * Time.deltaTime / Mathf.Sqrt(2f));
                }
                else
                {
                    transform.Translate(move * activeMoveSpeed * Time.deltaTime);
                }
                isMoving = true;
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }
        animator.SetBool("isMoving", isMoving);
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            isMoving = false;
            GetComponent<AudioSource>().Stop();
        }

        if (Input.GetKey(KeyCode.F))
        {
            Interact();
        }

        Flicker();

        if (Input.GetMouseButtonDown(1))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }

        }
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = speed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

    }

    private void Die()
    {
        if (buttonOut.activeSelf == true)
        {
            outDungeon.GetComponent<StartDungeon>().startDungeon = transform.position;
            buttonOut.SetActive(false);
        }
        SetPos();
    }
    protected void SetPos()
    {
        transform.position = lastMove;
        health = maxHealth;
    }

    protected void Interact()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1f, NPC);


        if (collider != null)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(collider);
            }
        }
    }
    protected void StopAttack()
    {
        transform.Find("SpriteInHand").gameObject.SetActive(false);
        check = 0;
    }

    public void UnderAttack(float damage)
    {

        if (health - damage >= 0)
        {
            health -= damage;
            if (!hurtSourceF.isPlaying)
            {
                hurtSourceF.PlayOneShot(hurtSound);
            }
        }
        else
        {
            health = 0;
            if (!dieSound.isPlaying)
            {
                dieSound.Play();
            }
        }
        hurtPlayer = true;
        hurt = timeHurt;
    }

    public float MaxHealth()
    {
        return maxHealth;
    }

    protected void Flicker()
    {
        if (hurtPlayer)
        {
            if ((int)(100 * hurt / 16) % 2 == 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            if (hurt <= 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                hurtPlayer = false;
            }
            hurt -= Time.deltaTime;
        }
    }

    protected void HandleExpChange(int amount)
    {
        currentExp += amount;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    protected void LevelUp()
    {
        animator.SetBool("levelUp", true);
        Invoke("AnimatorLevelUp", 0.3f);
        currentLevel++;
        currentExp -= maxExp;
        maxExp = (int)(1.1 * maxExp);
        maxHealth = (int)(1.1 * maxHealth);
        health = maxHealth;
        gameObject.GetComponent<Player>().baseAttack += 10;
    }
    protected void AnimatorLevelUp()
    {
        animator.SetBool("levelUp", false);
    }

    internal void AddHealth(int val)
    {
        float newHealth = health + val;
        health = Mathf.Clamp(newHealth, 0, maxHealth);
    }

}
