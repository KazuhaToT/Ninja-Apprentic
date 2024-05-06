using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    FreeRoam,
    Dialog,
    Inventory,
    GameOver,
    Option,
    IndexPlayer
}
public class GameController : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected Abilities abilities;

    GameState gameState;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () => gameState = GameState.Dialog;

        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (gameState == GameState.Dialog)
            gameState = GameState.FreeRoam;
        };
        InventoryController.Instance.OnShowInventory += () => gameState = GameState.Inventory;
        InventoryController.Instance.OnCloseInventory += () =>
        {
            if (gameState == GameState.Inventory)
            gameState = GameState.FreeRoam;
        };
        GameOver.Instance.OnShowDialog += () => gameState = GameState.GameOver;
        GameOver.Instance.OnCloseDialog += () =>
        {
            if (gameState == GameState.GameOver)
            gameState = GameState.FreeRoam;
        };
        OptionPlay.Instance.OnShowDialog += () => gameState = GameState.Option;
        OptionPlay.Instance.OnCloseDialog += () =>
        {
            if (gameState == GameState.Option)
            gameState = GameState.FreeRoam;
        };
        IndexController.Instance.OnShowInventory += () => gameState = GameState.IndexPlayer;
        IndexController.Instance.OnCloseInventory += () =>
        {
            if (gameState == GameState.IndexPlayer)
            gameState = GameState.FreeRoam;
        };
        ShopInventory.Instance.OnShowInventory += () => gameState = GameState.Inventory;
        ShopInventory.Instance.OnCloseInventory += () =>
        {
            if (gameState == GameState.Inventory)
            gameState = GameState.FreeRoam;
        };
}

    protected void Update()
    {
        if (gameState == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
            abilities.HandleUpdate();
        }
        else if (gameState == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (gameState == GameState.GameOver)
        {
            GameOver.Instance.HandleUpdate();
        }
    }
}
