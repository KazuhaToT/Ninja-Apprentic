    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected Vector2 minBounds;
    [SerializeField] protected Vector2 maxBounds;

    [SerializeField] protected GameObject player;

    public bool isChasing;
    public bool isAttack;
    private Rigidbody2D rb;
    private Vector2 movement;
    protected Animator animator;
    protected EnemyAI enemyAI;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        minBounds = new Vector2(-1, -1) + (Vector2)transform.position;
        maxBounds = new Vector2(1, 1) + (Vector2)transform.position;
    }

    private void FixedUpdate()
    {
        if (enemyAI.getHealth < enemyAI.MaxHealth * enemyAI.MultiplesHealth)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            float distanceFromPlayer = Vector2.Distance(rb.position, player.transform.position);
            if (distanceFromPlayer <= 7f)
            {
                transform.position = Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.fixedDeltaTime);
                Vector2 direction = player.transform.position - (Vector3)rb.position;
                SetDirection(direction);
                if (distanceFromPlayer <= 1f)
                {
                    isAttack = true;
                }
                else
                {
                    isAttack = false;
                }
            }
            else
            {
                isChasing = false;
                enemyAI.SetHP();
            }
        }
        else if (rb.position.x < minBounds.x || rb.position.x > maxBounds.x || rb.position.y < minBounds.y || rb.position.y > maxBounds.y)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            SetDirection(movement);
        }
    }

    public void MoveTo(Vector2 position)
    {


        if (rb.position.x < minBounds.x)
        {
            movement = new Vector2(1, 0);
        }
        else if (rb.position.x > maxBounds.x)
        {
            movement = new Vector2(-1, 0);
        }
        else if (rb.position.y < minBounds.y)
        {
            movement = new Vector2(0, 1);
        }
        else if (rb.position.y > maxBounds.y)
        {
            movement = new Vector2(0, -1);
        }

    }
    protected void SetDirection(Vector2 directionF)
    {
        animator.SetFloat("moveX", directionF.x != 0 ? directionF.x / Mathf.Abs(directionF.x) : 0);
        animator.SetFloat("moveY", directionF.y != 0 ? directionF.y / Mathf.Abs(directionF.y) : 0);
    }
}
