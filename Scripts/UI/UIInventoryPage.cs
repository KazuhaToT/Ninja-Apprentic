using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] protected UIInventoryItem itemPrefab;
    [SerializeField] protected RectTransform content;
    [SerializeField] protected UIInventoryDescription itemDescription;
    [SerializeField] protected MounseFollower mouseFollower;
    [SerializeField] protected InventorySO inventoryGold;
    [SerializeField] protected TMP_Text goldText;

    List<UIInventoryItem> items = new List<UIInventoryItem>();
    protected int currentlyDraggedItemIndex = -1;
    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnItemSwapped;
    [SerializeField] protected ItemActionPanel itemActionPanel;
    private void Awake()
    {
        // if (gameObject.tag == "Inventory")
        {
            Hide();
        }
        // else
        // {
        //     InitializeInventoryUI(inventoryGold.Size);
        //     Show();
        //     foreach (var item in inventoryGold.GetCurrentInventoryState())
        //     {
        //         UpdateData(item.Key, item.Value.item.lootSprite, item.Value.quantity);
        //     }
        // }
        mouseFollower.Toggle(false);
        if (itemDescription != null)
        {
            itemDescription.ResetDescription();
        }
    }
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(content, false);
            items.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseBtnClicked += HandleShowItemActions;
        }
    }
    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (items.Count > itemIndex)
        {
            items[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleShowItemActions(UIInventoryItem item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(UIInventoryItem item)
    {
        ResetDraggtedItem();
    }

    private void HandleSwap(UIInventoryItem item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnItemSwapped?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(item);
    }
    protected void ResetDraggtedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
        // return;
    }
    private void HandleBeginDrag(UIInventoryItem item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;
        HandleItemSelection(item);
        OnStartDragging?.Invoke(index);
    }
    public void CreateDraggedItem(Sprite itemImage, int quantity)
    {
        mouseFollower.SetData(itemImage, quantity);
        mouseFollower.Toggle(true);
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        // if (gameObject.tag == "Inventory")
        {
            gameObject.SetActive(true);
        }
        if (itemDescription != null)
        {
            itemDescription.ResetDescription();
        }

        ResetSelection();
    }
    public void ResetSelection()
    {
        if (itemDescription != null)
        {
            itemDescription.ResetDescription();
        }
        DeselectAllItems();
    }
    public void AddAction(string actionName, Action performAction)
    {
        itemActionPanel.AddButon(actionName, performAction);
    }

    public void ShowItemAction(int itemIndex)
    {
        itemActionPanel.Toggle(true);
        itemActionPanel.transform.position = items[itemIndex].transform.position;
    }

    protected void DeselectAllItems()
    {
        foreach (var item in items)
        {
            item.Deselect();
        }
        itemActionPanel.Toggle(false);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggtedItem();
        itemActionPanel.Toggle(false);
    }

    internal void UpdateDescription(int obj, Sprite lootSprite, string lootName, string description, int price, bool isShop)
    {
        if (itemDescription != null)
        {
            itemDescription.SetDescription(lootSprite, lootName, description, price, isShop);
        }
        DeselectAllItems();
        items[obj].Select();

    }
    public void SetGold()
    {
        goldText.text = inventoryGold.gold.ToString();
    }

    internal void ResetAllItem()
    {
        foreach (var item in items)
        {
            item.ResetData();
            item.Deselect();
        }
    }
    private void Update()
    {
        SetGold();
    }
}

