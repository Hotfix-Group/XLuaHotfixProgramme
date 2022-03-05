using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 改变钻石数的指令
/// 和金币指令不同，在消息体里已经包含钻石数量
/// command和mediator总有一个要和proxy通信
/// </summary>

public class AlterDiamondCommand : PureMVC.Patterns.SimpleCommand
{
    public class Data
    {
        public DiamondEvent DiamondEvent = DiamondEvent.None;
        public int alterDiamondNum = 0;
        //获取消息体里技能类型及消耗等信息
        public Data(DiamondEvent tempDiamondEvent,int tempAlterDiamondNum)
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
        Debug.Log("player diamond alter by skill "+data.DiamondEvent+",the num is: "+data.alterDiamondNum);

        PlayerdataProxy playerdataProxy = (PlayerdataProxy)ApplicationFacade.Instance.RetrieveProxy(PlayerdataProxy.NAME);

        playerdataProxy.UpdateResource((ResourceType)2, data.alterDiamondNum);
    }


}

