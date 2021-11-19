using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
