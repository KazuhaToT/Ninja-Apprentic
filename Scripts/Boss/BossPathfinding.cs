using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPathfinding : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected Vector2 minBounds;
    [SerializeField] protected Vector2 maxBounds;

    [SerializeField] protected GameObject player;

    public bool isChasing;
    public bool isAttack;
    private Rigidbody2D rb;
    private Vector2 movement;
    protected Boss boss;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        minBounds = new Vector2(-1, -1) + (Vector2)transform.position;
        maxBounds = new Vector2(1, 1) + (Vector2)transform.position;
    }

    private void FixedUpdate()
    {
        if (boss.health < boss.maxHealth)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
        if (!boss.isSkill)
        {
            if (isChasing)
            {
                float distanceFromPlayer = Vector2.Distance(rb.position, player.transform.position);
                if (distanceFromPlayer <= 10f)
                {
                    transform.position = Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.fixedDeltaTime);
                }
                else
                {
                    isChasing = false;
                    boss.SetHP();
                }
            }
            else if (rb.position.x < minBounds.x || rb.position.x > maxBounds.x || rb.position.y < minBounds.y || rb.position.y > maxBounds.y)
            {
                MoveTo();
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            }
        }
    }

    public void MoveTo()
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
}
