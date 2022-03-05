using PureMVC.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//改变金币数字的指令

public class AlterCoinCommand : PureMVC.Patterns.SimpleCommand
{
    //资源种类为金币
    public ResourceType resourceType = ResourceType.Coin;
    // Start is called before the first frame update
    public class Data
    {
        public int coinEvent = 0;
        //获取消息体里金币事件
        public Data(int tempCoinEvent)
        {
            coinEvent = tempCoinEvent;
        }
    }

    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        Data data = notification.Body as Data;
        PlayerdataProxy playerProxy = Facade.RetrieveProxy(PlayerdataProxy.NAME) as PlayerdataProxy;
        BulletdataProxy BulletProxy = Facade.RetrieveProxy(BulletdataProxy.NAME) as BulletdataProxy;
        PrizedataProxy prizeProxy = Facade.RetrieveProxy(PrizedataProxy.NAME) as PrizedataProxy;
        //根据消息体增减金币数
        switch (data.coinEvent)
        {
            case -2:
                playerProxy.UpdateResource(resourceType, prizeProxy.Prizedata.BossPrizeCoin);
                break;
            case -1:
                playerProxy.UpdateResource(resourceType, prizeProxy.Prizedata.FishPrizeCoin);
                break;
            case 0 :
                playerProxy.UpdateResource(resourceType, Random.Range(0, 100));
                break;
            case 1 :
                playerProxy.UpdateResource(resourceType, BulletProxy.Bulletdata.Level1Cost);
                break;
            case 2:
                playerProxy.UpdateResource(resourceType, BulletProxy.Bulletdata.Level2Cost);             
                break;
            case 3:
                playerProxy.UpdateResource(resourceType, BulletProxy.Bulletdata.Level3Cost);
                break;
            default:
                break;
        }
    }
}
