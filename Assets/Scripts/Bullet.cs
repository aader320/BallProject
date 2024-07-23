using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f; // Bullet lifetime in seconds
    public float bulletSpeed = 10f;
    public float maxSpreadAngle = 5f; // Maximum deviation in degrees from the aim direction
    void Start()
    {
        // random spread angle
        float spreadAngle = Random.Range(-maxSpreadAngle, maxSpreadAngle);

        // Convert mouse position to world position at bullet's creation time
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion spreadRotation = Quaternion.Euler(0, 0, spreadAngle);
        mouseWorldPosition.z = 0; // Ensure it's in 2D

        // Calculate direction from bullet to mouse cursor location
        Vector2 shootingDirection = (mouseWorldPosition - transform.position).normalized;

        Vector2 spreadDirection = spreadRotation * shootingDirection;


        // Apply velocity in the shooting direction
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = spreadDirection * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody2D component.");
        }

        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(gameObject, lifetime); // Destroy the bullet after its lifetime
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Here you can add what happens when your bullet collides with another object
        // For now, we'll just destroy the bullet on collision

        if (collision.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }*/
}