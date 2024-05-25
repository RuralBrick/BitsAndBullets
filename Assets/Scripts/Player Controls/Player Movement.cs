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
    private Collider2D player_collider;

    // Decalre the Vector 2
    Vector2 move;
    Vector2 orientation;

    float dash_cooldown = 3f;
    float dash_duration = 0.1f;
    float dash_speed = 5f;
    bool dash_available = true;
    bool is_dashing = false;


    Vector2 dash_dir = Vector2.zero;
    int bullet_layer;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch the RigidBody from the game object
        rb = gameObject.GetComponent<Rigidbody2D>();
        player_collider = gameObject.GetComponent<Collider2D>();
        animator = gameObject.GetComponent<Animator>();
        gun = gameObject.GetComponentInChildren<GunBehavior>();
        gun.owner = this;

        // Assign value to move vector - 0 for now
        move = new Vector2(0, 0);
        orientation = new Vector2(1, 0);
        bullet_layer = LayerMask.NameToLayer("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        // Moving Code -------------------------------------------------------------------------------------------
        if (is_dashing)
        {
            rb.velocity = dash_dir * playerSpeed * dash_speed;
            return;
        }

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

    void OnDash()
    {
        if (dash_available)
        {
            is_dashing = true;
            dash_available = false;
            dash_dir = move;

            animator.SetTrigger("DashStart");
            Invoke("EndDash", dash_duration);
            IgnoreBulletCollisions(true);
        }
    }

    void EndDash()
    {
        is_dashing = false;
        animator.SetTrigger("DashEnd");
        Invoke("DashAvailable", dash_cooldown);
        ScoreManager.instance.StartDashTimer(this);
        IgnoreBulletCollisions(false);
    }

    void DashAvailable()
    {
        dash_available = true;
    }

    void IgnoreBulletCollisions(bool ignore)
    {
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == bullet_layer)
            {
                Physics2D.IgnoreCollision(player_collider, collider, ignore);
            }
        }
    }

    public void GetHit(BulletHitInfo hitInfo)
    {
        Debug.Log($"I, {playerName}, have been hit!");
        ScoreManager.instance.AddPoint(enemy);
        ScoreManager.instance.ResetIcons();
        GameOverManager.instance.PlayerWins(enemy);
    }
}
