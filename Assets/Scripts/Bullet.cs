using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20.0f; // The speed of the bullet

    public float damage = 20;

    void Start()
    {
        // Add force to the bullet in the direction it is facing
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}