using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter:MonoSingleton<EventCenter>
{
    private Dictionary<string, UnityAction> eventDic = new Dictionary<string, UnityAction>();

    public void AddEventListener(string name,UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name] += action;
        }
        else
        {
            eventDic.Add(name,action);
        }
    }

    public void Dispatch(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name].Invoke();
        }
    }
}
