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
    }

    public class SpawnerGroupBehavior : MonoBehaviour
    {
        [SerializeField] PowerupInfo[] powerups = new PowerupInfo[]
        {
            new PowerupInfo()
            {
                name = "Burst Fire",
                activator = delegate(PlayerMovement player) { Debug.Log($"(Pretend that I gave {player.playerName} burst fire)"); }
            },
            new PowerupInfo()
            {
                name = "Change Bullet Velocity",
                activator = delegate(PlayerMovement player) { Debug.Log($"(Pretend that I changed {player.playerName}'s bullet velocity)"); }
            },
            new PowerupInfo()
            {
                name = "Shield",
                activator = delegate(PlayerMovement player) { Debug.Log($"(Pretend that I gave {player.playerName} a shield)"); }
            }
        };

        [SerializeField] float startDelaySeconds = 10f;
        [SerializeField] float spawnMinIntervalSeconds = 8f;
        [SerializeField] float spawnMaxIntervalSeconds = 10f;
        
        SpawnerBehavior[] spawners;

        void Start()
        {
            spawners = GetComponentsInChildren<SpawnerBehavior>();
            Invoke("SpawnPowerup", startDelaySeconds);
        }

        void SpawnPowerup()
        {
            spawners[Random.Range(0, spawners.Length)].SpawnPickup(powerups[Random.Range(0, powerups.Length)]);
            Invoke("SpawnPowerup", Random.Range(spawnMinIntervalSeconds, spawnMaxIntervalSeconds));
        }
    }
}
