using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && CheckCollisionAbove()) {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") {
            collision.gameObject.transform.SetParent(null);
        }
    }

    private bool CheckCollisionAbove()
    {
        Collider2D coll = gameObject.GetComponent<Collider2D>();
        
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, .1f, playerLayer);
    }
}
