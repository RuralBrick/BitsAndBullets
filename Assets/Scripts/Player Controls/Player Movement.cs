using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Initialize Variables and constants
    [SerializeField] private float playerSpeed = 5; // Speed
    [SerializeField] private float reloadTimer = 3; // Reload timer

    // Rigid body
    private Rigidbody2D rb;

    // Initialize the Vector 2
    Vector2 move;
    




    // Start is called before the first frame update
    void Start()
    {
        // Assign rigidbody to the player
        rb = GetComponent<Rigidbody2D>();

        // Assign value to move vector
        move = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Set the rigidbody velocity to the input direction
        rb.velocity = move * playerSpeed;

        // Rotate to put is in the direction of motion - ONLY if we are moving
        if (move != Vector2.zero)
        {
            rb.transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.Atan2(-move.x, move.y) * Mathf.Rad2Deg);
        }

        // If we are reloading, reduce the reload timer - scaled to framerate
        if (reloadTimer > 0)
        {
            reloadTimer -= 1;
        }
    }



    // Get the move input
    void OnMove(InputValue value)
    {
        // Get the 2D vector
        move = value.Get<Vector2>();
    }

    
    // When we hit the fire key - no input, just a button push
    void OnFire()
    {
        // If we are still reloading
        if (reloadTimer != 0)
        {
            // Then do not fire
            Debug.Log("Still reloading!");
            return;
        }

        // Otherwise - fire

        Debug.Log("Firing");

        // Set reload timer
    }


}
