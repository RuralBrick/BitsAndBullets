using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    const float MAX_X_DISTANCE = 1000f;
    const float MAX_Y_DISTANCE = 1000f;

    public PlayerMovement sourcePlayer;

    [SerializeField] float bulletSpeed = 10f;

    new Rigidbody2D rigidbody;

    ContactPoint2D[] contactPoints = new ContactPoint2D[10];

    public Vector3 BulletSpriteDirection
    {
        get => transform.right;
        set { transform.right = value; }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 displacement = BulletSpriteDirection * bulletSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(transform.position + displacement);
        if (Mathf.Abs(transform.position.x) > MAX_X_DISTANCE
            || Mathf.Abs(transform.position.y) > MAX_Y_DISTANCE)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var hitInfo = new BulletHitInfo()
            {
                sourcePlayer = sourcePlayer,
                damage = 1
            };
            collision.gameObject
                     .GetComponent<PlayerMovement>()
                     .GetHit(hitInfo);
            Destroy(gameObject);
        }
        else
        {
            Vector2 contactNormal = Vector2.zero;
            for (int i = 0; i < collision.GetContacts(contactPoints); i++)
            {
                contactNormal += contactPoints[i].normal;
            }
            contactNormal = contactNormal.normalized;
            BulletSpriteDirection = Vector3.Reflect(BulletSpriteDirection, contactNormal);
        }
    }
}
