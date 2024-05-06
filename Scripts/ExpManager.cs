using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        GameObject expManagerObject = new GameObject("ExpManager");
        Instance = expManagerObject.AddComponent<ExpManager>();
    }

    public delegate void ExpChangeHandler(int amount);
    public event ExpChangeHandler OnExpChange;

    public void AddExp(int amount)
    {
        OnExpChange?.Invoke(amount);
    }
}
