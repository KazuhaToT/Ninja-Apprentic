using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPlay : MonoBehaviour
{
    [SerializeField] protected GameObject options;
    [SerializeField] private GameObject button;
    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public static OptionPlay Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void OnClickBackOption()
    {
        OnShowDialog?.Invoke();
        options.transform.parent.gameObject.SetActive(true);
        button.SetActive(false);
    }
    public void SetOffOption()
    {
        OnCloseDialog?.Invoke();
        options.transform.parent.gameObject.SetActive(false);
        button.SetActive(true);
    }
}
