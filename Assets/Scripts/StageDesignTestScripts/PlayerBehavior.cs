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

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void OnMove(InputValue value)
        {
            move = value.Get<Vector2>();
        }

        void OnFire()
        {
            if (move != Vector2.zero)
            {
                Instantiate(
                    bulletPrefab,
                    transform.position + (Vector3)move.normalized,
                    Quaternion.Euler(0, 0, Mathf.Atan2(-move.x, move.y) * Mathf.Rad2Deg)
                );
            }
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
