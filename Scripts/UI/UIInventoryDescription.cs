using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TMP_Text title;
    [SerializeField] protected TMP_Text description;

    public void Awake()
    {
        ResetDescription();
    }
    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.title.text = "";
        this.description.text = "";
    }
    public void SetDescription(Sprite sprite, string title, string description, int price, bool isShop)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.title.text = title;
        if (isShop)
        {
            this.description.text = description + "Price: " + price.ToString() + " gold";
            return;
        }
        this.description.text = description;
    }
}
