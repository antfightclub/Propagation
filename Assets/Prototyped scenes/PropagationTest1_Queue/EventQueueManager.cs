using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EventQueue 
{
    public class EventQueueManager
    {
        private static EventQueueManager _instance;
        public static EventQueueManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventQueueManager();
                }
                return _instance;
            }
        }

        public delegate void EventDelegateX<T>(T e) where T : GameEvent;
        private delegate void EventDelegateX(GameEvent e);

        private Dictionary<System.Type, EventDelegateX> DelegatesMap = new Dictionary<System.Type, EventDelegateX>();
        private Dictionary<System.Delegate, EventDelegateX> DelegateLookupMap = new Dictionary<System.Delegate, EventDelegateX>();

        public void AddListener<T>(EventDelegateX<T> del) where T : GameEvent
        {
            EventDelegateX internalDelegate = (e) => { del((T)e); };
            if (DelegateLookupMap.ContainsKey(del) && DelegateLookupMap[del] == internalDelegate)
            {
                return;
            }
            DelegateLookupMap[del] = internalDelegate;

            EventDelegateX tempDel;
            if (DelegatesMap.TryGetValue(typeof(T), out tempDel))
            {
                DelegatesMap[typeof(T)] = tempDel += internalDelegate;
            }
            else
            {
                DelegatesMap[typeof(T)] = internalDelegate;
            }
        }

        public void RemoveListener<T>(EventDelegateX<T> del) where T : GameEvent
        {
            EventDelegateX internalDelegate;
            if (DelegateLookupMap.TryGetValue(del, out internalDelegate))
            {
                EventDelegateX tempDel;
                if (DelegatesMap.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        DelegatesMap.Remove(typeof(T));
                    }
                    else
                    {
                        DelegatesMap[typeof(T)] = tempDel;
                    }
                }

                DelegateLookupMap.Remove(del);
            }
        }

        public void AddEventToQueue(GameEvent e)
        {
            EventDelegateX del;
            if (DelegatesMap.TryGetValue(e.GetType(), out del))
            {
                del.Invoke(e);
            }
        }

    }

    public interface IMessageEvent
    {
        DateTime timeRaised { get; }
        float distanceToPlayer { get; }
        object message { get; }
    }

    public class MessageEvent : GameEvent, IMessageEvent
    {
        public DateTime timeRaised { private set; get; }
        public float distanceToPlayer { private set; get; }
        public object message { private set; get; }

        public MessageEvent(object message, float distanceToPlayer)
        {
            this.message = message;
            this.distanceToPlayer = distanceToPlayer;
            timeRaised = DateTime.Now;
        }
    }

    public abstract class GameEvent
    {

    }
}