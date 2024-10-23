using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EventQueue
{
    public class ObserverEventQueue : MonoBehaviour
    {

        private static Vector3 playerPosition;

        #region eventqueue

        private List<IMessageEvent> pendingEventQueueList = new List<IMessageEvent>();

        void OnAddEventToQueue(IMessageEvent e)
        {
            pendingEventQueueList.Add(e);
        }

        void Update()
        {
            for (int i = pendingEventQueueList.Count - 1; i >= 0; i--)
            {
                if (System.DateTime.Now.Subtract(pendingEventQueueList[i].timeRaised).Seconds > pendingEventQueueList[i].distanceToPlayer)
                {
                    
                    Debug.Log("Message received [" + System.DateTime.Now + "]: " + pendingEventQueueList[i].message.ToString());
                    pendingEventQueueList.RemoveAt(i);
                }
                    
            }
        }

        void Start()
        {
            if (pendingEventQueueList.Count > 0)
            {
                pendingEventQueueList.Clear();
            }

            EventQueueManager.Instance.AddListener<MessageEvent>(OnAddEventToQueue);
        }

        void OnDisable()
        {
            EventQueueManager.Instance.RemoveListener<MessageEvent>(OnAddEventToQueue);
        }

        #endregion

        void OnEnable()
        {
            // Call some function that sets up?
            playerPosition = this.transform.position;
        }
        


        public static float GetDistanceToPlayer(UnityEngine.Vector3 v)
        {
            float difference = (v - playerPosition).magnitude;
            return difference;
        }

    }
}
