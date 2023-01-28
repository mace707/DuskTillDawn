using UnityEngine;

public class Enemy : MonoBehaviour
{
    // The amount of health the enemy has
    public float health = 100f;

    // The amount of damage the enemy will take when it is hit
    public float damage = 10f;

    // The explosion prefab that will be instantiated when the enemy is destroyed
    public GameObject explosionPrefab;

    void Start()
    {
        // Find the explosion prefab if it was not set in the inspector
        if (explosionPrefab == null)
        {
            explosionPrefab = Resources.Load<GameObject>("Explosion");
        }
    }

    // This function is called when the enemy collides with a bullet
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision was with a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Take damage equal to the bullet's damage
            TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
        }
    }

    // This function is called to make the enemy take damage
    public void TakeDamage(float damage)
    {
        // Reduce the enemy's health by the amount of damage taken
        health -= damage;

        // If the enemy's health is less than or equal to zero, destroy it
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    // This function is called when the enemy is destroyed
    void DestroyEnemy()
    {
        // Instantiate the explosion prefab at the enemy's position
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

        // Destroy the explosion prefab after 2 seconds
        Destroy(explosion, 2f);

        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
