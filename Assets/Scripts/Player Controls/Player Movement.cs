using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public string playerName;
    public PlayerMovement enemy;

    // Declare Variables and Constants
    [SerializeField] private float playerSpeed = 5; // Speed

    // Declare Rigid body
    private Rigidbody2D rb;
    private Animator animator;
    private GunBehavior gun;

    // Decalre the Vector 2
    Vector2 move;
    Vector2 orientation;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch the RigidBody from the game object
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        gun = gameObject.GetComponentInChildren<GunBehavior>();
        gun.owner = this;

        // Assign value to move vector - 0 for now
        move = new Vector2(0, 0);
        orientation = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Moving Code -------------------------------------------------------------------------------------------

        // Set the rigidbody velocity to the input direction
        rb.velocity = move * playerSpeed;
        animator.SetFloat("NormalizedSpeed", move.magnitude);

        // Rotate to put is in the direction of motion - ONLY if we are moving
        if (move != Vector2.zero)
        {
            if (move.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);

            // Also store this orientation for firing purposes
            orientation = move.normalized;
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
        gun.Fire(orientation);
    }

    void OnReset()
    {
        GameOverManager.instance.ResetGame();
    }

    public void GetHit(BulletHitInfo hitInfo)
    {
        Debug.Log($"I, {playerName}, have been hit!");
        ScoreManager.instance.AddPoint(enemy);
        GameOverManager.instance.PlayerWins(enemy);
    }
}
