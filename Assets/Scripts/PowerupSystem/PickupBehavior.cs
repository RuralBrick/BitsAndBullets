using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerupSystem
{
    public class PickupBehavior : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var player = collision.GetComponent<PlayerMovement>();
                Debug.Log($"{player.playerName} has picked up a powerup!");
                // TODO: Activate appropriate ability
                Destroy(gameObject);
            }
        }
    }
}
