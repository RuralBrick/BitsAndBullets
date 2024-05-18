using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StageDesignTestScripts
{
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] float movementSpeed;
        [SerializeField] float bulletSpeed;

        public string playerName;

        [SerializeField] GameObject bulletPrefab;

        new Rigidbody2D rigidbody;

        Vector2 move;
        Vector2 lastMove = Vector2.right;

        public int ammo = 3;
        public float fireRate = 1f;
        private float nextFireTime = 0f;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void OnMove(InputValue value)
        {
            move = value.Get<Vector2>();

            if (move != Vector2.zero)
            {
                lastMove = move;
            }
            
        }

        void OnFire()
        {
            if (ammo > 0 && Time.time > nextFireTime)
            {
                Instantiate(
                    bulletPrefab,
                    transform.position + (Vector3)lastMove.normalized,
                    Quaternion.Euler(0, 0, Mathf.Atan2(-lastMove.x, lastMove.y) * Mathf.Rad2Deg)
                );
                ammo--;
                nextFireTime = Time.time + fireRate;
            }
  
        }

        public bool canFire()
        {
            return Time.time > nextFireTime;
        }

        // Update is called once per frame
        void Update()
        {
            rigidbody.velocity = move * movementSpeed;
        }

        public void GetHit(BulletHitInfo hitInfo)
        {
            Debug.Log($"I, {playerName}, have gotten hit!");
        }
    }
}
