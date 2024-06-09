using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private int direction = -1;

    private Rigidbody2D rb;
    private SpriteRenderer mySpriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
        Debug.Log(rb.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        float enimyX = transform.position.x;

        if (enimyX < minX) {
            direction = 1;
            mySpriteRenderer.flipX = true;
        } else if (enimyX > maxX) {
            direction = -1;
            mySpriteRenderer.flipX = false;
        }
        
        rb.velocity = new Vector2(speed, 0) * direction;
    }
}
