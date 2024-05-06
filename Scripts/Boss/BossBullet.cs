using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Vector3 startPosition;
    private float maxDistance = 15f;

    public void SetTarget(PlayerController target, float damage)
    {
        startPosition = transform.position;
        StartCoroutine(MoveToTarget(target, damage));
    }

    private IEnumerator MoveToTarget(PlayerController target, float damage)
    {
        while (Vector3.Distance(transform.position, target.transform.position) > 0.1f)
        {
            if (Vector3.Distance(transform.position, startPosition) > maxDistance)
            {
                gameObject.SetActive(false);
                yield break;
            }
            if (Vector3.Distance(transform.position, target.transform.position) <= 0.1f)
            {
                gameObject.SetActive(false);
                //gây damage cho player
                target.UnderAttack(damage);
                yield break;
            }

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 3.5f * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
