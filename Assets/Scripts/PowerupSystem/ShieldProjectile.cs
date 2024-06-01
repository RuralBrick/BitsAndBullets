using UnityEngine;

[System.Obsolete("Depreciated. Not necessary.")]
public class ShieldProjectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has the "Bullet" tag
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Calculate reflection direction
            Vector2 incomingVelocity = collision.relativeVelocity;
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflection = Vector2.Reflect(incomingVelocity, normal);

            // Apply the reflection to the bullet
            Rigidbody2D bulletRigidbody = collision.rigidbody;
            bulletRigidbody.velocity = reflection;
        }
    }
}