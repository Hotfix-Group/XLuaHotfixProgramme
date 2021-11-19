using PureMVC.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVersionCommand : PureMVC.Patterns.SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        //获取当前版本信息proxy
        VersionProxy version = Facade.RetrieveProxy(VersionProxy.NAME) as VersionProxy;
        if (version != null)
        {
            
        }
    }
}
