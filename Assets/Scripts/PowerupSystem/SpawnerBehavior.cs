using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerupSystem
{
    public class SpawnerBehavior : MonoBehaviour
    {
        [SerializeField] GameObject pickupPrefab;

        PickupBehavior currentPickup;

        void Start()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        public void SpawnPickup()
        {
            if (currentPickup != null)
            {
                Debug.Log("Tried spawning pickup, but there already was one");
                return;
            }
            var pickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity).GetComponent<PickupBehavior>();
            // TODO: Initialize pickup
            currentPickup = pickup;
        }
    }
}
