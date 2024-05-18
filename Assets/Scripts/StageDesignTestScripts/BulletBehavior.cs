using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StageDesignTestScripts
{
    public class BulletBehavior : MonoBehaviour
    {
        const float MAX_X_DISTANCE = 1000f;
        const float MAX_Y_DISTANCE = 1000f;

        [SerializeField] float bulletSpeed;

        new Rigidbody2D rigidbody;

        ContactPoint2D[] contactPoints = new ContactPoint2D[10];

        Vector3 GetBulletSpriteDirection()
        {
            return transform.up;
        }

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            Vector3 displacement = GetBulletSpriteDirection() * bulletSpeed * Time.fixedDeltaTime;
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
                collision.gameObject
                         .GetComponent<PlayerBehavior>()
                         .GetHit(new BulletHitInfo { damage = 1 });
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
                transform.up = Vector3.Reflect(GetBulletSpriteDirection(), contactNormal);
            }
        }
    }
}
