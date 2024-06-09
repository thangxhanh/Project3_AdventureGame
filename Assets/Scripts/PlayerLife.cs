using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    
    [SerializeField] private AudioSource soundSfx;
    [SerializeField] private AudioClip death;
    [SerializeField] private float minValueY = -10f;
    [SerializeField] private bool dead = false;
        
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (transform.position.y < minValueY && !dead) {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        soundSfx.PlayOneShot(death);
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
