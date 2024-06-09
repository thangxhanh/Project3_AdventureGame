using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    
    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource soundSfx;
    [SerializeField] private AudioClip collection;

    public float scaleSmallFactor = 0.5f;
    public float scaleLargeFactor = 1f;

    private Vector3 originalScale;
    private bool isTransformed = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry")) {
            soundSfx.PlayOneShot(collection);
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }

        if(collision.gameObject.CompareTag("Kiwi"))
        {

            Destroy(collision.gameObject);
            transform.localScale = originalScale * scaleSmallFactor;
            isTransformed = true;
        }

        if (isTransformed && collision.gameObject.CompareTag("Melon"))
        {

            Destroy(collision.gameObject);
            transform.localScale = originalScale;
            isTransformed = false;
        }
    }
}
