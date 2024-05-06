using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] public List<InventoryItem> inventoryItems;
    [SerializeField] private int size = 10;
    [SerializeField] public int gold = 0;
    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public int Size
    {
        get { return size; }
        private set { size = value; }
    }

    public void Initialize()
    {
        inventoryItems.Clear();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
            
        }
    }

    public int AddItem(LootManager item, int quantity, List<ItemParameter> itemState = null)
    {
        if (!item.isStackable)
        {
            foreach (InventoryItem v in inventoryItems)
            {
                while (quantity > 0 && IsInventoryFull() == false)
                {
                    quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                }
                InformAboutChange();
                return quantity;
            }
        }
        quantity = AddStackableItem(item, quantity);
        InformAboutChange();
        return quantity;
    }

    private int AddItemToFirstFreeSlot(LootManager item, int quantity, List<ItemParameter> itemState = null)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            quantity = quantity,
            itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
        };
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = newItem;
                return quantity;
            }
        }
        return 0;
    }

    private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

    private int AddStackableItem(LootManager item, int quantity)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                continue;
            }
            if (inventoryItems[i].item.ID == item.ID)
            {
                int amountPossible = inventoryItems[i].item.maxSizeStack - inventoryItems[i].quantity;
                if (amountPossible < quantity)
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.maxSizeStack);
                    quantity -= amountPossible;
                }
                else
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                    InformAboutChange();
                    return 0;
                }
            }
        }
        while (quantity > 0 && IsInventoryFull() == false)
        {
            int newQuantity = Mathf.Clamp(quantity, 0, item.maxSizeStack);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity);
        }
        return quantity;
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                continue;
            }
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItem GetItemAt(int obj)
    {
        return inventoryItems[obj];
    }

    public void SwapItems(int arg1, int arg2)
    {
        InventoryItem item1 = inventoryItems[arg1];
        inventoryItems[arg1] = inventoryItems[arg2];
        inventoryItems[arg2] = item1;
        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }

    public void RemoveItem(int obj, int v)
    {
        if (inventoryItems.Count > obj)
        {
            if (inventoryItems[obj].IsEmpty)
                return;
            int reminder = inventoryItems[obj].quantity - v;
            if (reminder <= 0)
                inventoryItems[obj] = InventoryItem.GetEmptyItem();
            else
                inventoryItems[obj] = inventoryItems[obj]
                    .ChangeQuantity(reminder);

            InformAboutChange();
        }
    }

    internal void AddGold(int quantity)
    {
        gold += quantity;
    }
}

[System.Serializable]
public struct InventoryItem
{
    public int quantity;
    public LootManager item;
    public List<ItemParameter> itemState;
    public bool IsEmpty => item == null;

    public InventoryItem ChangeQuantity(int value)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = value,
            itemState = new List<ItemParameter>(this.itemState),
        };
    }
    public static InventoryItem GetEmptyItem()
    => new InventoryItem
    {
        item = null,
        quantity = 0,
        itemState = new List<ItemParameter>(),
    };
}

