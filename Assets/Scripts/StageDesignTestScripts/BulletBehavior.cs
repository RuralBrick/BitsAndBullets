using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageDesignTestScripts
{
    public class BulletBehavior : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerBehavior>().GetHit(new BulletHitInfo { damage = 1 });
                Destroy(gameObject);
            }
            // HACK
            Destroy(gameObject);
            // end HACK
        }
    }
}
