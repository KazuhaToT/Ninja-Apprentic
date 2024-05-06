using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        distance = Mathf.Clamp(distance, 0, 10f);
        float volume = 1 - (distance / 10f);
        GetComponent<AudioSource>().volume = volume;
    }
}
