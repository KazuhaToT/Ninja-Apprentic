using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public static GameOver Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected Image image;
    [SerializeField] protected Text text;
    public float fadeInTime = 1f;
    float alpha;

    public void Start()
    {
        image.GetComponent<Image>().enabled = false;
        text.GetComponent<Text>().enabled = false;
        alpha = 0;
    }
    public void SetOnShowDialog()
    {
        OnShowDialog?.Invoke();
    }
    public void HandleUpdate()
    {
        if (playerController.getHealth <= 0 && alpha < 255f)
        {
            image.GetComponent<Image>().enabled = true;
            text.GetComponent<Text>().enabled = true;
            alpha += fadeInTime * Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

        }
        else
        {
            RestartGame();
        }
    }
    protected void RestartGame()
    {
        alpha = 0;
        image.GetComponent<Image>().enabled = false;
        text.GetComponent<Text>().enabled = false;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        OnCloseDialog?.Invoke();
    }
}
