using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    public void Interact(Collider2D collider)
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, collider.name));
    }
}
