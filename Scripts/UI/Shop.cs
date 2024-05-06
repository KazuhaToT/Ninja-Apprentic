using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class Shop : MonoBehaviour
{
    [SerializeField] protected UIInventoryPage inventoryUI;
    [SerializeField] protected InventorySO inventoryData;
    [SerializeField] protected InventorySO inventoryData2;

    [SerializeField]
    private AudioClip dropClip;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField] GameObject imageWeapon;
    [SerializeField] protected GameObject player;

    private void Start()
    {
        PrepareUI();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PrepareUI();
    }
    public void SetDataTransformScene()
    {
        PrepareUI();
    }

    public void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequested;
        this.inventoryUI.OnItemSwapped += HandleItemSwapped;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequested;
    }

    private void HandleItemActionRequested(int obj)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(obj);
        if (inventoryItem.IsEmpty)
        {
            return;
        }
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            inventoryUI.ShowItemAction(obj);

        }
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.AddAction("Buy", () => BuyItem(obj, inventoryItem.quantity));
        }
    }
    private void BuyItem(int itemIndex, int quantity)
    {
        if (inventoryData.GetItemAt(itemIndex).item.price <= inventoryData2.gold)
        {
            inventoryData2.AddItem(inventoryData.GetItemAt(itemIndex).item, quantity);
            inventoryData2.gold -= inventoryData.GetItemAt(itemIndex).item.price;
        }
    }
    private string PrepareDescription(InventoryItem inventoryItem)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(inventoryItem.item.Description);
        sb.AppendLine();
        return sb.ToString();
    }

    private void HandleDragging(int obj)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(obj);
        if (inventoryItem.IsEmpty)
        {
            return;
        }
        inventoryUI.CreateDraggedItem(inventoryItem.item.lootSprite, inventoryItem.quantity);
    }

    private void HandleItemSwapped(int arg1, int arg2)
    {
        inventoryData.SwapItems(arg1, arg2);
    }

    private void HandleDescriptionRequested(int obj)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(obj);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        LootManager item = inventoryItem.item;
        string description = PrepareDescription(inventoryItem);
        inventoryUI.UpdateDescription(obj, item.lootSprite, item.lootName, description, inventoryItem.item.price, true);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventoryUI.isActiveAndEnabled == true)
            {
                inventoryUI.Hide();
            }
        }
    }
    public void OpenShop()
    {
        inventoryUI.Show();
        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.lootSprite, item.Value.quantity);
        }
    }
}
