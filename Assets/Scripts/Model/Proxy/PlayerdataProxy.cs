using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家数据代理
/// </summary>
public class PlayerdataProxy : PureMVC.Patterns.Proxy
{
    public new static string NAME = "PlayerData";

    public PlayerdataModel PlayerData;

    public PlayerdataProxy(string name) : base(name, null)
    {
        PlayerData = new PlayerdataModel();
    }
    public PlayerdataModel GetPlayerData
    {
        get
        {
            return Data as PlayerdataModel;
        }
    }


    public override void OnRegister()
    {
        Debug.Log("PlayerDataProxy OnRegister");
    }
    public override void OnRemove()
    {
        Debug.Log("PlayerDataProxy OnRemove");
    }
}
