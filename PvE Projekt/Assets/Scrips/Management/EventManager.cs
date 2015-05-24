using UnityEngine;
using System.Collections.Generic;
using System;

public class EventManager
{
    private Dictionary<Type, List<Action<EventArgs>>> eventHandlers = null;
    private static EventManager instance = null;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManager();
            return instance;    
        }
    }
    private EventManager()
    {
        eventHandlers = new Dictionary<Type,List<Action<EventArgs>>>();
    }

    public void Register<T>(Action<EventArgs> handler) where T : EventArgs
    {
        Type t = typeof(T);
        if (eventHandlers.ContainsKey(t))
            eventHandlers[t].Add(handler);
        else
        {
            List<Action<EventArgs>> li = new List<Action<EventArgs>>();
            li.Add(handler);
            eventHandlers.Add(t, li);
        }
    }

    public void Deregister<T>(Action<EventArgs> handler) where T: EventArgs
    {
        Type t = typeof(T);
        if (eventHandlers.ContainsKey(t))
            eventHandlers[t].Remove(handler);
    }

    /// <summary>
    /// Notifies all registered classes
    /// </summary>
    /// <typeparam name="T">the actual message type</typeparam>
    /// <param name="message">message content</param>
    public void SendMessage<T>(EventArgs message)
    {
        Type t = typeof(T);
        if (eventHandlers.ContainsKey(t))
        {
            List<Action<EventArgs>> handlers = eventHandlers[t];
            for (int i = 0; i < handlers.Count; i++)
                handlers[i](message);
        }
    }
}
