using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
/// <summary>
/// 游戏启动并初始化UI的Command
/// </summary>
public class StartUpCommand : PureMVC.Patterns.SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        //此处可接入luaUI框架，由lua端实现
        GameObject canvas = GameObject.Find("Canvas");
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/MainUI/UI_Main"),canvas.transform);
        if(obj == null)
        {
            Debug.Log("cannot find ui prefab");
            return;
        }
        obj.name = "UI_Main";
        Facade.RegisterMediator(new MainPanelMediator(obj));
        Debug.Log("start up command");
    }
}
