using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MounseFollower : MonoBehaviour
{
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected UIInventoryItem item;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }
    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }
    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out Vector2 localPoint);
        transform.position = canvas.transform.TransformPoint(localPoint);
    }
    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }
}
