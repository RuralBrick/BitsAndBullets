using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portals
{
    public class PortalGroupBehavior : MonoBehaviour
    {
        [SerializeField] Color color = Color.white;

        PortalBehavior[] portals;

        void Start()
        {
            portals = GetComponentsInChildren<PortalBehavior>();
            foreach(PortalBehavior p in portals)
            {
                p.SetColor(color);
            }
        }

        public void Send(PortalBehavior source, GameObject obj)
        {
            ShufflePortals();
            GameObject arrival = obj;
            foreach (PortalBehavior destination in portals)
            {
                if (destination == source)
                    continue;

                // Make a clone
                if (arrival == null)
                    arrival = Instantiate(obj);

                destination.Receive(arrival);
                arrival = null;

                if (obj.CompareTag("Player"))
                    break;
            }
        }

        void ShufflePortals()
        {
            List<PortalBehavior> oldPortalOrder = portals.ToList();
            portals = new PortalBehavior[portals.Length];
            int currentIndex = 0;
            while (currentIndex < portals.Length && oldPortalOrder.Count > 0)
            {
                int nextPick = Random.Range(0, oldPortalOrder.Count);
                portals[currentIndex++] = oldPortalOrder[nextPick];
                oldPortalOrder.RemoveAt(nextPick);
            }
        }
    }
}
