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
    new private Collider2D collider;

    // Decalre the Vector 2
    Vector2 move;
    Vector2 orientation;

    float dash_time = 0f;
    float dash_cooldown = 3f;
    float dash_speed = 5f;
    Vector2 dash_dir = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        // Fetch the RigidBody from the game object
        rb = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
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

        if (dash_time > 0f)
        {
            rb.velocity = dash_dir * playerSpeed * dash_speed;
            dash_time -= Time.deltaTime;
            return;
        }

        collider.enabled = true;
        // Set the rigidbody velocity to the input direction
        rb.velocity = move * playerSpeed;
        animator.SetTrigger("DashEnd");
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

        dash_cooldown -= Time.deltaTime;
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

    void OnDash()
    {
        if (dash_cooldown < 0f)
        {
            dash_time = 0.1f;
            dash_dir = move;
            dash_cooldown = 3f;
            ScoreManager.instance.StartDashTimer(this);
            animator.SetTrigger("DashStart");
            collider.enabled = false;
        }
    }

    public void GetHit(BulletHitInfo hitInfo)
    {
        Debug.Log($"I, {playerName}, have been hit!");
        ScoreManager.instance.AddPoint(enemy);
        GameOverManager.instance.PlayerWins(enemy);
    }
}
