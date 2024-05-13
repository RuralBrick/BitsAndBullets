using StageDesignTestScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Declare Prefabs that we use
    [SerializeField] GameObject bulletPrefab;

    


    // Declare Variables and Constants
    [SerializeField] private float playerSpeed = 5; // Speed
    [SerializeField] private float coolDown = 3; // Reload timer



    // Declare Rigid body
    private Rigidbody2D rb;

    // Declare Sprite Renderer
    private SpriteRenderer spriteRender;

    // Decalre the Vector 2
    Vector2 move;
    Vector2 orientation;

    // Declare the reloading counter
    private float reloadTimer;

    // Set bool for if in a wall
    private bool inWall;



    // Start is called before the first frame update
    void Start()
    {
        // Fetch the RigidBody from the game object
        rb = gameObject.GetComponent<Rigidbody2D>();

        // Fetch the Sprite renderer from the game object
        spriteRender = gameObject.GetComponent<SpriteRenderer>();

        // Assign a starting color - Green means ok to shoot
        spriteRender.color = Color.green;

        // Assign value to move vector - 0 for now
        move = new Vector2(0, 0);
        orientation = new Vector2(0, 1);

        // Assign the reload timer to the default value - 0 - ready to fire
        reloadTimer = 0;

        // CircleCollider2D bulletSpawn = GetComponent<CircleCollider2D>();
        // Set the inWall Boolean
        inWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Moving Code -------------------------------------------------------------------------------------------

        // Set the rigidbody velocity to the input direction
        rb.velocity = move * playerSpeed;

        // Rotate to put is in the direction of motion - ONLY if we are moving
        if (move != Vector2.zero)
        {
            // Set the rotation
            rb.transform.rotation = Quaternion.Euler(Vector3.forward * Mathf.Atan2(-move.x, move.y) * Mathf.Rad2Deg);

            // Also store this orientation for firing purposes
            orientation = move;
        }
        
        
        // Shooting and Reloading -------------------------------------------------------------------------------

        // If we are reloading, reduce the reload timer - scaled to framerate
        if (reloadTimer > 0)
        {
            reloadTimer -= 1 * Time.deltaTime;
        }

        // If the reload timer ever hits negative - unity moment - set it 0 to make it nice
        if (reloadTimer < 0)
        {
            reloadTimer = 0;
        }

        // If The reload timer is 0 and we are not already green, then change the color to green to signify that we are good to shoot
        if (spriteRender.color != Color.green && reloadTimer == 0)
        {
            spriteRender.color = Color.green;
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
        // If we are in a wall
        if (inWall == true)
        {
            // Then do not fire
            Debug.Log("In a wall - Can't Shoot");
            return;
        }


        // If we are still reloading
        if (reloadTimer > 0)
        {
            // Then do not fire
            Debug.Log("Still Reloading");
            return;
        }

        // Otherwise - fire - change color to red to show that we have to reload
        spriteRender.color = Color.red;

        // Instantiate the bullet prefab
        Instantiate(bulletPrefab, transform.position + (Vector3)orientation.normalized, rb.transform.rotation);

        Debug.Log("Firing");

        // Set reload timer
        reloadTimer = coolDown;
    }



    // Contact
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            inWall = true;
        }
    }

    // Leave Contact
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            inWall = false;
        }
    }


}
