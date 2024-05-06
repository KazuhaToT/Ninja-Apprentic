using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDungeon : MonoBehaviour
{
    [SerializeField] public Vector2 outDungeon;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject buttonOut;
    public void InitializeOutDungeon()
    {
        GetComponent<StartDungeon>().startDungeon = player.transform.position;
        player.transform.position = outDungeon;
        buttonOut.SetActive(false);
    }
}
