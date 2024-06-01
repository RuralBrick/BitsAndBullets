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
    // Assign value to move vector - 0 for now
    Vector2 move = new Vector2(0, 0);
    Vector2 orientation = new Vector2(1, 0);

    float dash_cooldown = 3f;
    float dash_duration = 0.1f;
    float dash_speed = 5f;
    bool dash_available = true;
    bool is_dashing = false;

    Vector2 dash_dir = Vector2.zero;
    int bullet_layer;


    // multiplier for reducing cooldown times
    public float cooldownMutliplier = 1f;


    // Start is called before the first frame update
    void Start()
    {
        // Fetch the RigidBody from the game object
        rb = gameObject.GetComponent<Rigidbody2D>();
        player_collider = gameObject.GetComponent<Collider2D>();
        animator = gameObject.GetComponent<Animator>();

        gun = gameObject.GetComponentInChildren<GunBehavior>();
        gun.owner = this;

        bullet_layer = LayerMask.NameToLayer("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        // update the cooldownMultiplier - used for updating cooldown sprites
        cooldownMutliplier = 3 / gun.returnBulletCooldown();


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
            FaceInDirection(move);
        }
    }

    public void FaceInDirection(Vector3 direction)
    {
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        // Also store this orientation for firing purposes
        orientation = direction.normalized;
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
            SoundEffectManager.Instance.PlaySound("dash");
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



    // Power ups ----------------------------------------------------------
    // Set the number of shots that we shoot to the input n
    public void setNumShots(int n)
    {
        gun.setNumShots(n);
    }

    // Increase the number of shots that we shoot by 1
    public void increaseNumShots()
    {
        gun.increaseNumShots();
    }

    // set the number of shots that we shoot to a random value between 1-10 (CHAOS!!! >:D)
    public void setNumShotsRAND()
    {
        gun.setNumShotsRAND();
    }

    // Set the bullet speed to the input n
    public void setBulletSpeed(float n)
    {
        gun.setBulletSpeed(n);
    }

    // Increase the bullet speed by the float n
    public void increaseBulletSpeed(float n)
    {
        gun.increaseBulletSpeed(n);
    }

    // set the bullet speed to a random value between 1 and 20
    public void setBulletSpeedRAND()
    {
        gun.setBulletSpeedRAND();
    }

    // set the bullet cooldown to the specified value
    public void setBulletCoolDown(float n)
    {
        gun.setBulletCoolDown(n);
    }

    // Decrease the bullet cooldown by 1 - goes down to 0
    public void decreaseBulletCoolDown()
    {
        gun.decreaseBulletCooldown();
    }

    //Shield
    public void DeployShield()
    {
        gun.DeployShield();
    }

    // Set teh bullet cooldown to 0.1
    public void machineGun()
    {
        gun.machineGun();
    }




}
