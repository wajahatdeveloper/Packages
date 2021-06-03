using System.Collections.Generic;
using UnityEngine;

public static class EventExtensions
{
    public delegate void EventWithData(GameObject sender, object data);

    /// <summary>
    ///     Look up Event Name
    ///     Look up GameObject
    ///     Look up EventHandlers
    /// </summary>
    public static Dictionary<string, Dictionary<GameObject, List<EventWithData>>> eventHandlers =
        new Dictionary<string, Dictionary<GameObject, List<EventWithData>>>();

    public static void ConnectEvent(this GameObject listener, string eventName, EventWithData func)
    {
        if (eventName.IsEmpty()) return;

        if (eventHandlers.ContainsKey(eventName))
        {
            if (eventHandlers[eventName].ContainsKey(listener))
            {
                if (eventHandlers[eventName][listener].Contains(func) == false)
                    eventHandlers[eventName][listener].Add(func);
            }
            else
            {
                eventHandlers[eventName].Add(listener, new List<EventWithData> {func});
            }
        }
        else
        {
            eventHandlers.Add(eventName, new Dictionary<GameObject, List<EventWithData>>());
            eventHandlers[eventName].Add(listener, new List<EventWithData> {func});
        }
    }

    public static void DisconnectEvent(this GameObject listener, string eventName)
    {
        if (eventHandlers.ContainsKey(eventName) == false)
        {
            Debug.LogError("Event System : Invalid Event Name : Unable to Disconnect Event Handler from Object " +
                           listener.name);
            return;
        }

        eventHandlers[eventName].Remove(listener);
    }

    public static void RaiseEvent(this GameObject sender, string eventName, object eventData = null)
    {
        if (eventHandlers.ContainsKey(eventName) == false)
        {
            Debug.LogError("Event System : Invalid Event Name : Unable to Raise Event from Object " + sender.name);
            return;
        }

        foreach (var item in eventHandlers[eventName])
        foreach (var handler in item.Value)
            handler(sender, eventData);
    }
}