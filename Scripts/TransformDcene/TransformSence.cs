using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransformSence : MonoBehaviour
{
    [SerializeField] public string sceneName;
    [SerializeField] public Vector3 playerPosition;
    [SerializeField] public SceneInfo sceneInfo;
    [SerializeField] public Player player;
    [SerializeField] public PlayerController playerController;
    [SerializeField] public InventorySO inventoryData;
    [SerializeField] public InventorySO saveData;
    [SerializeField] public InventoryController inventoryController;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetDataInventory();
            sceneInfo.SaveData(player, playerController);
            SceneManager.LoadScene(sceneName);
            // other.transform.position = playerPosition;
            // player.UpdateData(sceneInfo.dataPlayer.baseAttack, sceneInfo.dataPlayer.attack, sceneInfo.dataPlayer.speedAttack, sceneInfo.dataPlayer.critRate, sceneInfo.dataPlayer.critDamage, sceneInfo.dataPlayer.breakAmount);
            // playerController.UpdateData(sceneInfo.dataPlayer.maxHealth, sceneInfo.dataPlayer.health, sceneInfo.dataPlayer.currentExp, sceneInfo.dataPlayer.maxExp, sceneInfo.dataPlayer.currentLevel);
            // inventoryController.SetDataTransformScene();
        }
    }
    protected void SetDataInventory()
    {
        saveData.inventoryItems.Clear();
        foreach (var item in inventoryData.inventoryItems)
        {
            if(item.item != null)
            {
                saveData.inventoryItems.Add(item);
            }
        }
    }
}
