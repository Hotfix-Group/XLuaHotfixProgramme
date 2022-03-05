using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
/// <summary>
/// 玩家数据代理
/// 处理玩家金钱钻石数的初始化，以及玩家金钱钻石增减处理
/// </summary>
/// 
public enum ResourceType
{
    None,
    Coin = 1,
    Diamond = 2
}

public class PlayerdataProxy : PureMVC.Patterns.Proxy
{
    public new static string NAME = "PlayerData";
    //枚举定义消耗的资源类型，1为钻石，2为金币
 

    public SkilldataModel SkillData;
    public PlayerdataModel PlayerData;

    public PlayerdataProxy(string name) : base(name, null)
    {
        PlayerData = new PlayerdataModel();
    }
    //public PlayerdataProxy(string proxyName, object data = null) : base(proxyName, data)
    //{
    //}
    public PlayerdataModel GetPlayerData
    {
        get
        {
            return Data as PlayerdataModel;
        }
    }


    public override void OnRegister()
    {
        base.OnRegister();
        Debug.Log("PlayerDataProxy OnRegister");
    }
    public override void OnRemove()
    {
        base.OnRemove();
        Debug.Log("PlayerDataProxy OnRemove");
    }
    //玩家资源数值发生变动后的数据处理（钻石和金币）
    //传入参数为资源类型枚举、资源变动数值（正数为获得，负数为消耗）
    public void UpdateResource(ResourceType resourceType, int UpdateResourceNum)
    {
        switch (resourceType)
        {
            case ResourceType.Diamond:
                {
                    PlayerData.DiamondNum += UpdateResourceNum;
                }
                break;
            case ResourceType.Coin:
                {
                    PlayerData.CoinNum += UpdateResourceNum;
                }
                break;
            default:
                return;
        }
        if (PlayerData.CoinNum <= 0)
        {
            PlayerData.CoinNum = 0;
        }
        if (PlayerData.DiamondNum <= 0)
        {
            PlayerData.DiamondNum = 0;
        }
    }
}
