using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerupSystem
{
    public delegate void PowerupActivator(PlayerMovement player);

    [System.Serializable]
    public class PowerupInfo
    {
        public string name;
        public Sprite icon;
        public PowerupActivator activator;
        public int spawnOdds = 1;
    }

    public class SpawnerGroupBehavior : MonoBehaviour
    {
        [SerializeField] PowerupInfo[] powerups = new PowerupInfo[]
        {
            new PowerupInfo()
            {
                name = "Burst Fire",
                activator = delegate(PlayerMovement player)
                {
                    player.increaseNumShots();
                }
            },
            new PowerupInfo()
            {
                name = "Change Bullet Velocity",
                activator = delegate(PlayerMovement player)
                {
                    player.increaseBulletSpeed(5);
                }
            },
            new PowerupInfo()
            {
                name = "Shield",
                activator = delegate(PlayerMovement player)
                {
                    player.DeployShield();
                }
            },
            new PowerupInfo()
            {
                name = "Reduce Cooldown Time",
                activator = delegate(PlayerMovement player)
                {
                    player.machineGun();
                }
            }
        };

        [SerializeField] float startDelaySeconds = 5f;
        [SerializeField] float spawnMinIntervalSeconds = 3f;
        [SerializeField] float spawnMaxIntervalSeconds = 10f;

        List<PowerupInfo> powerupPool;
        SpawnerBehavior[] spawners;

        void Start()
        {
            powerupPool = new List<PowerupInfo>();
            foreach (var powerup in powerups)
            {
                for (int i = 0; i < powerup.spawnOdds; i++)
                {
                    powerupPool.Add(powerup);
                }
            }
            spawners = GetComponentsInChildren<SpawnerBehavior>();
            Invoke("SpawnPowerup", startDelaySeconds);
        }

        void SpawnPowerup()
        {
            var spawner = spawners[Random.Range(0, spawners.Length)];
            var powerup = powerupPool[Random.Range(0, powerupPool.Count)];
            spawner.SpawnPickup(powerup);
            Invoke("SpawnPowerup", Random.Range(spawnMinIntervalSeconds, spawnMaxIntervalSeconds));
        }
    }
}
