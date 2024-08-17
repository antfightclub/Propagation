using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObserverEventQueue : MonoBehaviour
{

    #region eventqueue

    private List<IMessageEvent> pendingEventQueueList = new List<IMessageEvent> ();

    void OnAddEventToQueue(IMessageEvent e)
    {
        pendingEventQueueList.Add(e);
        if (logToConsole)
        {
            Debug.Log("Message received [" + System.DateTime.Now + "]: " + e.message.ToString());
        }
    }

    void Update()
    {
        for (int i = pendingEventQueueList.Count - 1; i >= 0; i--)
        {
            if (Time.time > pendingEventQueueList[i].displayTime)
                pendingEventQueueList.RemoveAt(i);
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
        EventQueueManager.Instance.RemoveListener<MessageEvent>(OnAddEventToQueue)
    }

    #endregion

    void OnEnable()
    {
        // Call some function that sets up?
    }


}
