using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDungeon : MonoBehaviour
{
    [SerializeField] public Vector2 startDungeon = new Vector2(-56f, -88.5f);
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject buttonOut;
    public void InitializeDungeon()
    {
        Debug.Log("Initialize Dungeon");
        player.transform.position = startDungeon;
        buttonOut.SetActive(true);
    }
}
