using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    // Start is called before the first frame update

    public float knockbackStrength = 0.3f;
    public float speed = 5f;

    // component getters
    public GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer spriterenderer;


    // knockback filter
    private bool knockback = false;
    private float knockcounter = 0;

    //Ememy attributes
    private int health = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");

        if (knockback == false)
        {
            Vector3 direction = player.transform.position - transform.position;

            // Normalize the direction
            Vector3 normalizedDirection = direction.normalized;


            if (normalizedDirection.x > 0)
            {
                spriterenderer.flipX = true;

            }
            else if (normalizedDirection.x < 0)
            {
                spriterenderer.flipX = false;

            }

            rb.velocity = normalizedDirection; // moveSpeed controls the movement velocity

        }
        else
        {
            knockcounter += Time.deltaTime;

            if (knockcounter > 0.2)
            {
                knockcounter = 0;
                knockback = false;

            }
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Here you can add what happens when your bullet collides with another object
        // For now, we'll just destroy the bullet on collision


        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (knockback == false)
            {

                knockback = true;
                // Calculate the direction of the knockback
                Vector2 knockbackDirection = collision.transform.position - transform.position;
                knockbackDirection = -knockbackDirection.normalized; // Normalize to get direction only

                // Apply the knockback force
                rb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
            }
            health -= 1;
            Destroy(collision.gameObject);

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            knockback = true;
            // Calculate the direction of the knockback
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            knockbackDirection = -knockbackDirection.normalized; // Normalize to get direction only

            // Apply the knockback force
            rb.AddForce(knockbackDirection * (knockbackStrength * 0.5f), ForceMode2D.Impulse);


        }

    }

}
