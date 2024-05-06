using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;

public class IndexController : MonoBehaviour
{
    [SerializeField] protected GameObject playerIndex;
    public event Action OnShowInventory;
    public event Action OnCloseInventory;
    public static IndexController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (playerIndex.activeSelf)
            {
                playerIndex.SetActive(false);
                OnCloseInventory?.Invoke();
            }
            else
            {
                playerIndex.SetActive(true);
                OnShowInventory?.Invoke();
            }
        }
    }
}
