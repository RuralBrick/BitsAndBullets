using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StageDesignTestScripts
{
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] float movementSpeed;

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

        // Update is called once per frame
        void Update()
        {
            rigidbody.velocity = move * movementSpeed;
        }
    }
}
