using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class InventoryController : MonoBehaviour
{
    [SerializeField] protected UIInventoryPage inventoryUI;
    [SerializeField] protected InventorySO inventoryData;
    [SerializeField] protected InventorySO saveData;

    [SerializeField]
    private AudioClip dropClip;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField] GameObject imageWeapon;


    public event Action OnShowInventory;
    public event Action OnCloseInventory;
    public static InventoryController Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PrepareUI();
        PrepareIventoryData();
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
        PrepareIventoryData();
    }
    public void SetDataTransformScene()
    {
        PrepareUI();
        PrepareIventoryData();
    }
    protected void PrepareIventoryData()
    {
        inventoryData.inventoryItems.Clear();
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach (var item in saveData.inventoryItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            inventoryData.AddItem(item.item, item.quantity);
            if (item.item is EquippableItemSO itemE)
            {
                itemE.Initialize();
            }
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> dictionary)
    {
        inventoryUI.ResetAllItem();
        foreach (var item in dictionary)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.lootSprite, item.Value.quantity);
        }
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
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(obj));

        }
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.AddAction("Drop", () => DropItem(obj, inventoryItem.quantity));
        }
    }
    private void DropItem(int itemIndex, int quantity)
    {
        inventoryData.RemoveItem(itemIndex, quantity);
        inventoryUI.ResetSelection();
        audioSource.PlayOneShot(dropClip);
    }
    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
        }
        EquippableItemSO equippableItemSO = inventoryItem.item as EquippableItemSO;
        if(equippableItemSO != null){
            imageWeapon.GetComponent<Image>().sprite = equippableItemSO.lootSprite;
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
        inventoryUI.UpdateDescription(obj, item.lootSprite, item.lootName, description, 0, false);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                OnShowInventory?.Invoke();
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.lootSprite, item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
                OnCloseInventory?.Invoke();
            }
        }

    }
}
