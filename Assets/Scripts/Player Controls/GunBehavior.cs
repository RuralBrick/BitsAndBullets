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
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<SpriteRenderer>().color = owner.GetComponent<SpriteRenderer>().color;
        BulletBehavior bulletBehavior = newBullet.GetComponent<BulletBehavior>();
        bulletBehavior.BulletSpriteDirection = direction;
        bulletBehavior.sourcePlayer = owner;
        Invoke("FinishCooldown", coolDownSeconds);
        return true;
    }

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
