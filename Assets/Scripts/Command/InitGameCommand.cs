using PureMVC.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 场景切换完毕后初始化游戏界面的command
/// </summary>
public class InitGameCommand : PureMVC.Patterns.SimpleCommand
{
    private MainPanel view;
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        //获取资源UI
        GameObject GunUI = GameObject.Find("Canvas/UI_GunPanel");
        if(GunUI == null)
        {
            Debug.Log("cannot find gun ui");
            return;
        }
        Facade.RegisterMediator(new GunPanelMediator(GunUI));

        //获取技能UI
        GameObject skillUI = GameObject.Find("Canvas/UI_Skills");
        if(skillUI == null)
        {
            Debug.Log("cannot find skill ui");
            return;
        }
        Facade.RegisterMediator(new SkillPanelMediator(skillUI));
    
        //获取玩家数据
        PlayerdataProxy playerdataProxy = ApplicationFacade.Instance.RetrieveProxy(PlayerdataProxy.NAME) as PlayerdataProxy;
        PlayerdataModel playerData = playerdataProxy.GetPlayerData;
        Debug.Log("player coin:" + playerdataProxy.PlayerData.CoinNum + " player diamond：" + playerdataProxy.PlayerData.DiamondNum);

        //初始化玩家数据
        GunPanelMediator gunMediator = Facade.RetrieveMediator(GunPanelMediator.NAME) as GunPanelMediator;
        gunMediator.InitPlayerData();

        //获取技能数据
        SkilldataProxy skilldataProxy = ApplicationFacade.Instance.RetrieveProxy(SkilldataProxy.NAME) as SkilldataProxy;
        SkilldataModel skillData = skilldataProxy.GetSkillData;

        //获取子弹数据
        
        Debug.Log("init game command");
    }




}
