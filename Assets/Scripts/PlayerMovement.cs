using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public GameObject bulletPrefab; // Assign your bullet prefab in the inspector


    public float recoil = 0.01f;
    private float recoilcal = 0f;
    private SpriteRenderer spriterenderer;
    private Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (recoilcal < recoil)
        {
            recoilcal += Time.deltaTime;
        }

        if (Input.GetButton("Fire1")) // "Fire1" is typically the left mouse button
        {
            if (recoilcal > recoil)
            {
                ShootBullet();
                recoilcal = 0;
            }
        }

        if(mousePos.x > transform.position.x)
        {
            spriterenderer.flipX = true;
        }else if(mousePos.x < transform.position.x)
        {
            spriterenderer.flipX = false;
        }

        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if( moveDirection.x!=0 || moveDirection.y != 0)
        {
            animate.Play("Base Layer.MOVING");
        }
        else
        {
            animate.Play("Base Layer.IDLE");
        }




    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void ShootBullet()
    {


       Instantiate(bulletPrefab, transform.position, Quaternion.identity);


    }
}
