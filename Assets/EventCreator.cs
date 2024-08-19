using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace EventQueue
{
    public class EventCreator : MonoBehaviour
    {

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                float distanceToPlayer = ObserverEventQueue.GetDistanceToPlayer(this.transform.position);
                EventQueueManager.Instance.AddEventToQueue(new MessageEvent("Hiii :3 I said hiii :3 at " + System.DateTime.Now + ", and the distance to player is " + distanceToPlayer, distanceToPlayer));
            }
        }

    }
}
