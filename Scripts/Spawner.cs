using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected List<(GameObject enemyAI, float respawnTime)> respawnQueue = new List<(GameObject, float)>();

}
