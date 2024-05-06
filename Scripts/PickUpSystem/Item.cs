using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [field: SerializeField] public LootManager InventoryItem { get; set; }
    [field: SerializeField] public int Quantity { get; set; } = 1;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected float duration = 0.3f;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.lootSprite;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickUp());
    }


    private IEnumerator AnimateItemPickUp()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(0, 0, 0);
        float currentTime = 0.0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
