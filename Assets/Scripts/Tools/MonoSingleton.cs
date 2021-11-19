using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 继承mono的单例基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T Instance = null;

    public static T GetInstance()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<T>();
            if(FindObjectsOfType<T>().Length > 1)
            {
                return Instance;
            }
            if(Instance == null)
            {
                string instanceName = typeof(T).Name;
                GameObject instanceGameObject = GameObject.Find(instanceName);
                if (instanceGameObject == null)
                    instanceGameObject = new GameObject(instanceName);
                Instance = instanceGameObject.AddComponent<T>();
                //避免切换场景时GO被销毁
                DontDestroyOnLoad(instanceGameObject);
            }
            //问题：切换场景时会不会创建新的instanceGameObject？
        }
        return Instance;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        DontDestroyOnLoad(gameObject);
        Init();
    }
    public void StartUp()
    {
        Debug.Log(string.Format("{0} is startup...", gameObject.name));
    }

    protected virtual void Init()
    {

    }
    protected virtual void OnDestroy()
    {
        Instance = null;
    }
}
