using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    public PlayerMovement owner;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Color readyColor = Color.green;
    [SerializeField] Color coolDownColor = Color.red;
    [SerializeField] float coolDownSeconds = 3f;
    private float numShots = 1;    // How many bullets we shoot per shot - DEFAULT
    private float bulletSpeed = 10f;    // How fast does each bullet move - DEFAULT

    SpriteRenderer spriteRenderer;

    uint numObstaclesInside = 0;
    bool coolingDown = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = readyColor;
    }

    public bool Fire(Vector3 direction)
    {
        if (numObstaclesInside > 0 || coolingDown)
            return false;
        coolingDown = true;
        spriteRenderer.color = coolDownColor;


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
        return true;
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
