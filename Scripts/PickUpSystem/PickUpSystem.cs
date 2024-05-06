using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] public InventorySO inventory;
    protected int lv ;
    private void Start() {
        lv = gameObject.GetComponent<PlayerController>().currentLevel;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            if (item.InventoryItem.lootName == "gold")
            {
                inventory.AddGold(Random.Range(500*(1+lv/10), 1000*(1+lv/10)));
                item.DestroyItem();
            }
            else
            {
                int reminder = inventory.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                {
                    item.DestroyItem();
                }
                else
                {
                    item.Quantity = reminder;
                }
            }
        }
    }
}
