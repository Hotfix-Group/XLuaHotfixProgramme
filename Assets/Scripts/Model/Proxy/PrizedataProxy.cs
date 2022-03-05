using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  打怪奖励数据代理 
/// </summary>
public class PrizedataProxy : PureMVC.Patterns.Proxy
{
    public new static string NAME = "PrizeData";

    public PrizedataModel Prizedata;
    public PrizedataProxy(string name) : base(name, null)
    {
        Prizedata = new PrizedataModel();
    }
    public override void OnRegister()
    {
        base.OnRegister();
        Debug.Log("PrizedataProxy OnRegister");
    }
    public override void OnRemove()
    {
        base.OnRemove();
        Debug.Log("PrizedataProxy OnRemove");
    }

}
