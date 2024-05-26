using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerupSystem
{
    public class SpawnerGroupBehavior : MonoBehaviour
    {
        [SerializeField] float startDelaySeconds = 10f;
        [SerializeField] float spawnMinIntervalSeconds = 8f;
        [SerializeField] float spawnMaxIntervalSeconds = 10f;
        [SerializeField] SpawnerBehavior[] spawners;

        void Start()
        {
            Invoke("SpawnPowerup", startDelaySeconds);
        }

        void SpawnPowerup()
        {
            // TODO: Choose random powerup
            spawners[Random.Range(0, spawners.Length)].SpawnPickup();
            Invoke("SpawnPowerup", Random.Range(spawnMinIntervalSeconds, spawnMaxIntervalSeconds));
        }
    }
}
