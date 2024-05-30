using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Portals
{
    public class PortalBehavior : MonoBehaviour
    {
        PortalGroupBehavior group;

        List<GameObject> arrivals = new List<GameObject>();

        private void Start()
        {
            group = GetComponentInParent<PortalGroupBehavior>();
        }

        public void SetColor(Color color)
        {
            GetComponent<SpriteRenderer>().color = color;
            var particles = GetComponentInChildren<ParticleSystem>().main;
            particles.startColor = color;
        }

        public void Receive(GameObject obj)
        {
            arrivals.Add(obj);
            obj.transform.position = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (arrivals.Contains(collision.gameObject))
                return;
            if (collision.CompareTag("Player")
                || collision.CompareTag("Bullet")
                || collision.CompareTag("Obstacle"))
            {
                group.Send(this, collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            arrivals.Remove(collision.gameObject);
        }
    }
}
