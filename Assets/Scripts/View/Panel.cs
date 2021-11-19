using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panel面板基类，所有面板的共有特性包含在此
/// </summary>

public abstract class Panel : MonoBehaviour
{
    // Start is called before the first frame update
   protected  virtual void Start()
    {
        InitPanel();
        InitDataAndSetComponentState();
        RegisterComponent();
        RegisterCommand();
        RegisterMediator();
    }
    //面板初始化
    protected abstract void InitPanel();
    //面板数据、参数初始化
    protected abstract void InitDataAndSetComponentState();
    //注册面板对象
    protected abstract void RegisterComponent();
    //注册Command
    protected abstract void RegisterCommand();
    //注册Mediator
    protected abstract void RegisterMediator();

    public virtual void OnDestroy()
    {
        UnRegisterComponent();
        UnRegisterCommand();
        UnRegisterMediator();
    }
    //注销
    protected abstract void UnRegisterComponent();
    protected abstract void UnRegisterCommand();
    protected abstract void UnRegisterMediator();

}
