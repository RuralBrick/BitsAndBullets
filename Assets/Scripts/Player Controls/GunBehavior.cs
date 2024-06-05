using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    public PlayerMovement owner;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] Color readyColor = Color.green;
    [SerializeField] Color coolDownColor = Color.red;
    [SerializeField] float coolDownSeconds = 3.1f;    // How long it takes to reload after you shoot - DEFAULT
    [SerializeField] float numShots = 1;    // How many bullets we shoot per shot - DEFAULT
    [SerializeField] float bulletSpeed = 10f;    // How fast does each bullet move - DEFAULT
    [SerializeField] float shieldSpeed = 50f;


    SpriteRenderer spriteRenderer;

    uint numObstaclesInside = 0;
    bool coolingDown = false;
    bool shieldActive = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = readyColor;
    }

    public bool Fire(Vector3 direction)
    {
        if (numObstaclesInside > 0 || coolingDown)
            return false;

        if (shieldActive)
        {
            shieldActive = false;
            FireShield(direction);
            ScoreManager.instance.RemoveIcon("barrier", owner);
        }
        else
        {
            FireBullet(direction);
        }

        return true;
    }

    void FireBullet(Vector3 direction)
    {
        coolingDown = true;
        spriteRenderer.color = coolDownColor;
        SoundEffectManager.Instance.PlaySound("fire");

        // Declare some vectors for used for calculating multiple shots
        Vector3 zDir = new Vector3(0, 0, 1);
        Vector3 xDir = new Vector3(1, 0, 0);
        Vector3 yDir = new Vector3(0, 1, 0);
        Vector3 bulletSpawn = Vector3.Cross(direction, zDir);

        // Shoot
        for (int i = 0; i < numShots; i++)
        {
            // Where do we put mutliple shots - position and orientation
            Vector3 bulletSpacing = (bulletSpawn * (0.25f * i - 0.125f * (numShots - 1)));
            Vector3 bulletOrientation = Vector3.zero;

            // Add appropiate spacing if we are shooting up
            if (direction != xDir && direction != (-1f * xDir))
            {
                bulletSpacing += 0.6f * direction;
            }

            // Add appropiate bullet spread
            if (direction.x >= 0 && direction.y == 0)
            {
                bulletOrientation -= (yDir * (0.05f * i - 0.025f * (numShots - 1)));
            }
            else if (direction.x < 0 && direction.y == 0)
            {
                bulletOrientation += (yDir * (0.05f * i - 0.025f * (numShots - 1)));
            }
            else if (direction.y >= 0)
            {
                bulletOrientation += (xDir * (0.05f * i - 0.025f * (numShots - 1)));
            }
            else if (direction.y < 0)
            {
                bulletOrientation -= (xDir * (0.05f * i - 0.025f * (numShots - 1)));
            }

            // Spawn the bullet
            GameObject newBullet = Instantiate(bulletPrefab, transform.position + bulletSpacing, Quaternion.identity);

            // Give it bullet behavior
            BulletBehavior bulletBehavior = newBullet.GetComponent<BulletBehavior>();

            // Give it direction
            bulletBehavior.BulletSpriteDirection = direction + bulletOrientation;

            // Give it the player that shot it
            bulletBehavior.sourcePlayer = owner;

            // Set the appropiate bullet speed
            bulletBehavior.changeBulletSpeed(bulletSpeed);
        }

        ScoreManager.instance.StartBulletTimer(owner);
        // Set the proper timer
        Invoke("FinishCooldown", coolDownSeconds);
    }

    void FireShield(Vector3 direction)
    {
        GameObject newShield = Instantiate(
            shieldPrefab,
            transform.position + 0.5f * direction,
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction))
        );
        newShield.GetComponent<Rigidbody2D>().velocity = direction * shieldSpeed;
    }

    // Functions for Shotgun ---------------------------------------------
    // change the number of bullets that we shoot per shot
    public void setNumShots(int num)
    {
        numShots = num;
    }

    // Increase the number of bullets that we shoot per shot
    public void increaseNumShots()
    {
        numShots += 1;
    }

    // Set the number of bullets that we shoot to a random number between 1 and 10
    public void setNumShotsRAND()
    {
        numShots = Random.Range(1, 10);
    }


    // Functions for bullet speed ------------------------------------------
    // Increase the bullet speed by the provided input
    public void increaseBulletSpeed(float input)
    {
        bulletSpeed += input;
    }

    // Set the bullet speed to the provided input
    public void setBulletSpeed(float input)
    {
        bulletSpeed = input;
    }

    // Set the bullet speed to a RANDOM value (1-20)
    public void setBulletSpeedRAND()
    {
        bulletSpeed = Random.Range(1, 20);
    }


    // Functions for bullet cooldown -------------------------------------------
    // Decrease the bullet cooldown by 1
    public void decreaseBulletCooldown()
    {
        Debug.Log("Previous Cooldown time: " + coolDownSeconds);
        // Only decrease the cooldown if we have the space to do so
        if (coolDownSeconds > 1.0f)
        {
            coolDownSeconds -= 1.0f;
        }
        Debug.Log("New Cooldown time: " + coolDownSeconds);
    }

    public void DeployShield()
    {
        shieldActive = true;
    }

    // Set the bulletCooldown to 0.1 - Machine gun!
    public void machineGun()
    {
        coolDownSeconds = 0.1f;
    }

    // Set the bulletCooldown to the specified value
    public void setBulletCoolDown(float n)
    {
        coolDownSeconds = n;
    }

    // return the current bullet cool down
    public float returnBulletCooldown()
    {
        return coolDownSeconds;
    }






    // Other stuff ------------------------------------------------------------
    void FinishCooldown()
    {
        spriteRenderer.color = readyColor;
        coolingDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        numObstaclesInside++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        numObstaclesInside--;
    }
}
