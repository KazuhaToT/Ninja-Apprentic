using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] protected GameObject droppedItemPrefab;
    [SerializeField] protected List<LootManager> lootTable = new List<LootManager>();

    protected LootManager GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<LootManager> possibleItems = new List<LootManager>();
        foreach (LootManager item in lootTable)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            int item = Random.Range(0, possibleItems.Count);
            return possibleItems[item];
        }
        return null;
    }
    public void InstantiateLoot(Vector3 spawnPosition)
{
    LootManager droppedItem = GetDroppedItem();
    if (droppedItem != null)
    {
        GameObject loot = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
        if (droppedItem is EquippableItemSO equippableItem)
        {
            EquippableItemSO clonedItem = Instantiate(equippableItem);
            clonedItem.Initialize();
            loot.GetComponent<Item>().InventoryItem = clonedItem;
        }
        else
        {
            loot.GetComponent<Item>().InventoryItem = droppedItem;
        }
    }
}

}
