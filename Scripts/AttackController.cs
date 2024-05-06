using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    protected Animator animator;
    protected Vector2 input;
    protected GameObject player;
    protected EnemyAI enemyAI;

    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip attackSound;
    [SerializeField]protected float dame;
    protected Player playerStats;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        player = transform.parent.gameObject;
        playerStats = player.GetComponent<Player>();
        input = player.GetComponent<PlayerController>().Weapon;
        RunAnimation();
        dame = playerStats.baseAttack * (1 + playerStats.attack/100);
        CritDamage();
    }
    public void CritDamage(){
        int randomNumber = Random.Range(0, 100);
        if (randomNumber <= playerStats.critRate)
        {
            dame = dame * (playerStats.critDamage/100);
        }
    }
    protected void RunAnimation()
    {
        if (Mathf.Abs(input.x) == Mathf.Abs(input.y))
        {
            input.x = 0;
        }
        else if (input.x == 0 && input.y == 0)
        {
            input.y = -1;
        }
        if (input != Vector2.zero)
        {
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAI>().UnderAttack(dame);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }
        if(collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().UnderAttack(dame);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }
    }


}
