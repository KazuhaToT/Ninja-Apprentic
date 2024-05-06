using UnityEngine;

public class EnemySpawner : Spawner
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] protected float respawnTime = 5f;

    private void Update()
    {
        for (int i = 0; i < respawnQueue.Count; i++)
        {
            var item = respawnQueue[i];
            item.respawnTime -= Time.deltaTime;
            respawnQueue[i] = item;
            if (respawnQueue[i].respawnTime <= 0)
            {
                RespawnEnemy(respawnQueue[i].enemyAI);
                respawnQueue.RemoveAt(i);
            }
        }
    }
    public void AddEnemyToRespawnQueue(GameObject enemy)
    {
        if (!IsEnemyInRespawnQueue(enemy))
        {
            respawnQueue.Add((enemy, respawnTime));
        }
    }

    protected bool IsEnemyInRespawnQueue(GameObject enemy)
    {
        foreach (var tuple in respawnQueue)
        {
            if (tuple.enemyAI == enemy)
                return true;
        }
        return false;
    }
    protected void RespawnEnemy(GameObject enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            EnemyAI position = enemy.gameObject.GetComponent<EnemyAI>();

            Vector2 initialPosition = position.initialPosition;

            enemy.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            enemy.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            enemy.GetComponentInChildren<Canvas>().enabled = true;
            position.SetHP();
            position.checkDie = false;

            enemy.transform.position = initialPosition;
        }
        else if (enemy.gameObject.CompareTag("Boss"))
        {
            Boss position = enemy.gameObject.GetComponent<Boss>();
            Vector2 initialPosition = position.initialPosition;
            enemy.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            enemy.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            enemy.GetComponentInChildren<Canvas>().enabled = true;
            position.SetHP();
            position.checkDie = false;
            enemy.transform.position = initialPosition;
        }
    }
}
