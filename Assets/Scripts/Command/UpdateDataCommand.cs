using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDataCommand : PureMVC.Patterns.SimpleCommand
{
    public class Data
    {
        public DiamondEvent DiamondEvent = DiamondEvent.None;
        public int alterDiamondNum = 0;
        //获取消息体里技能类型及消耗等信息
        public Data(DiamondEvent tempDiamondEvent, int tempAlterDiamondNum)
        {
            DiamondEvent = tempDiamondEvent;
            alterDiamondNum = tempAlterDiamondNum;
        }      
    }
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        Data data = notification.Body as Data;
        if (data == null)
        {
            return;
        }
        Debug.Log("player diamond alter by skill " + data.DiamondEvent + ",the num is: " + data.alterDiamondNum);

        PlayerdataProxy playerdataProxy = (PlayerdataProxy)ApplicationFacade.Instance.RetrieveProxy(PlayerdataProxy.NAME);
        playerdataProxy.UpdateResource((ResourceType)1, data.alterDiamondNum);
        PlayerdataModel playerData = playerdataProxy.GetPlayerData;
        ApplicationFacade.Instance.SendNotification(ApplicationFacade.ALTER_DIAMOND, playerData.DiamondNum);
    }
}
