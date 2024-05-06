using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] protected GameObject dialogBox;
    [SerializeField] protected Text dialogText;
    [SerializeField] protected Text nameText;
    [SerializeField] protected float letterPerSecond;
    [SerializeField] protected ShopController shop;
    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    public IEnumerator ShowDialog(Dialog text, string nameNPC)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        this.dialog = text;
        dialogBox.SetActive(true);
        nameText.text = nameNPC;
        StartCoroutine(TypeDialog(text.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                currentLine = 0;
                Debug.Log(nameText.text);
                if (nameText.text == "Kita")
                {
                    GetComponent<StartDungeon>().InitializeDungeon();
                }
                if(nameText.text.Contains("Shop")){
                    shop.OpenShop();
                }
                OnCloseDialog?.Invoke();

            }
        }
    }

    public IEnumerator TypeDialog(string lines)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in lines.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTyping = false;
    }
}