using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns;
/// <summary>
/// 枪和资源面板中介
/// </summary>

public class GunPanelMediator : PureMVC.Patterns.Mediator
{
    public new static string NAME = "GunPanelMediator";

    private GunPanel view;

    public ResourceType resourceType;

    PlayerdataProxy playerdataProxy;


    public GunPanelMediator(object viewComponent) : base(NAME, viewComponent)
    {
        GameObject gunPanel = GameObject.Find("Canvas/UI_GunPanel");
        if (gunPanel == null)
        {
            Debug.Log("cannot find gun view,init error");
        }
        playerdataProxy = Facade.RetrieveProxy(PlayerdataProxy.NAME) as PlayerdataProxy;
        view = ((GameObject)ViewComponent).GetComponent<GunPanel>();
        Debug.Log("Gun panel mediator");
    }
    protected GunPanel GetGunPanel
    {
        get
        {
            return ViewComponent as GunPanel;
        }
    }

    public override IList<string> GetListNotificationInterests()
    {
        IList<string> list = new List<string>()
        { ApplicationFacade.ALTER_COIN,
          ApplicationFacade.ALTER_DIAMOND};
        return list;
    }
    public void InitPlayerData()
    {
        Debug.Log("init player data");
        PlayerdataProxy playerdataProxy = ApplicationFacade.Instance.RetrieveProxy(PlayerdataProxy.NAME) as PlayerdataProxy;
        PlayerdataModel playerData = playerdataProxy.GetPlayerData;
        view.CoinNumber.text = playerdataProxy.PlayerData.CoinNum.ToString();
        view.DiamondNumber.text = playerdataProxy.PlayerData.DiamondNum.ToString();
    }

    //收到钻石金钱变动的通知，刷新数字
    public override void HandleNotification(INotification notification)
    {
        PlayerdataProxy playerdataProxy = ApplicationFacade.Instance.RetrieveProxy(PlayerdataProxy.NAME) as PlayerdataProxy;
        PlayerdataModel playerData = playerdataProxy.GetPlayerData;
        base.HandleNotification(notification);
   
        switch (notification.Name)
        {
            case ApplicationFacade.ALTER_COIN:
                {
                    //GetGunPanel.CoinNumber.text = playerData.DiamondNum.ToString();
                    view.CoinNumber.text = playerdataProxy.PlayerData.CoinNum.ToString();
                }
                break;
            case ApplicationFacade.ALTER_DIAMOND:
                {
                    //GetGunPanel.DiamondNumber.text = playerData.DiamondNum.ToString();
                    view.DiamondNumber.text = playerdataProxy.PlayerData.DiamondNum.ToString();
                }
                break;
        }
    }
}

