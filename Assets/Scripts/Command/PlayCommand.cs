using PureMVC.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCommand : PureMVC.Patterns.SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        
        SceneManager.LoadScene(1);
        //开始方法不能放在这里，因为还没进入到游戏场景
       // LuaManager.GetInstance().GameStart();
        Debug.Log("play command");
    }
}
