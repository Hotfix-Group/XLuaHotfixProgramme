using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class GunPanelMediator : PureMVC.Patterns.Mediator
{
    public new static string NAME = "GunPanelMediator";


    public GunPanelMediator(string mediatorName,object viewComponent = null):base(mediatorName, viewComponent)
    {

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
        { ApplicationFacade.OPEN_CHEST,ApplicationFacade.COST_DIAMOND};
        return list;
    }

    //收到钻石金钱变动的通知，刷新数值
    public override void HandleNotification(INotification notification)
    {
        PlayerdataProxy playerdataProxy = ApplicationFacade.Instance.RetrieveProxy(PlayerdataProxy.NAME) as PlayerdataProxy;
        PlayerdataModel playerData = playerdataProxy.GetPlayerData;
        base.HandleNotification(notification);
   
        switch (notification.Name)
        {
            case ApplicationFacade.COST_DIAMOND:
                {
                    GetGunPanel.DiamondNumber.text = playerData.DiamondNum.ToString();
                }
                break;
            case ApplicationFacade.OPEN_CHEST:
                {
                    GetGunPanel.CoinNumber.text = playerData.CoinNum.ToString();
                    GetGunPanel.DiamondNumber.text = playerData.DiamondNum.ToString();
                }
                break;
        }
    }
}
