using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] protected Shop inventoryUI1;
    [SerializeField] protected ShopInventory inventoryUI2;
    
    private void Awake() {
        inventoryUI1 = GetComponent<Shop>();
        inventoryUI2 = GetComponent<ShopInventory>();
    }
    public void OpenShop()
    {
        inventoryUI1.OpenShop();
        inventoryUI2.OpenShop();
    }
}
