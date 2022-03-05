using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 事件中心模块
/// </summary>

public class EventCenter:MonoSingleton<EventCenter>
{
    //key为事件名称，value为对应的委托函数
    //为了处理多参数的方法,需要将委托抽象为object类型（装拆箱的问题？看里氏替换原则）
    //private Dictionary<string, UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();
    private Dictionary<string, UnityAction> eventDic = new Dictionary<string, UnityAction>();

    public delegate void ssss(int cmd, object msg);
    //添加事件监听
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
    //事件触发 
    public void Dispatch(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name].Invoke();
        }
    }
    //移除事件监听
    public void RemoveEventListen(string name,UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name] -= action;
        }
    }
    //清空事件中心
    public void Clear()
    {
        eventDic.Clear();
    }
}
