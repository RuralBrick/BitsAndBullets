using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerupSystem
{
    public class PickupBehavior : MonoBehaviour
    {
        [SerializeField] Transform icon;
        [SerializeField] float periodSeconds = 1.75f;
        [SerializeField] float amplitudeUnits = 0.05f;

        PowerupInfo info = new PowerupInfo()
        {
            name = "Undefined",
            activator = delegate(PlayerMovement player) { Debug.Log("Oops! There was no powerup set to this pickup!"); }
        };
        float startHeight = 0f;

        private void Start()
        {
            startHeight = icon.localPosition.y;
        }

        private void Update()
        {
            float height = amplitudeUnits * Mathf.Sin(2 * Mathf.PI * Time.time / periodSeconds) + startHeight;
            icon.localPosition = new Vector3(icon.localPosition.x, height, icon.localPosition.z);
        }

        public void Initialize(PowerupInfo info)
        {
            this.info = info;
            icon.GetComponent<SpriteRenderer>().sprite = info.icon;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var player = collision.GetComponent<PlayerMovement>();
                Debug.Log($"{player.playerName} has picked up the {info.name} powerup!");
                SoundEffectManager.Instance.PlaySound("powerupCollect");
                info.activator(player);
                Destroy(gameObject);
            }
        }
    }
}
