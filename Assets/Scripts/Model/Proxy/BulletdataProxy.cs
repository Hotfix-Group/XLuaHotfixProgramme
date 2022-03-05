using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹消耗代理
/// </summary>

public class BulletdataProxy : PureMVC.Patterns.Proxy
{
    public new static string NAME = "BulletData";

    public BulletdataModel Bulletdata;

    public BulletdataProxy(string name) : base(name, null)
    {
        Bulletdata = new BulletdataModel();
    }
    public override void OnRegister()
    {
        base.OnRegister();
        Debug.Log("BulletdataProxy OnRegister");
    }
    public override void OnRemove()
    {
        base.OnRemove();
        Debug.Log("BulletdataProxy OnRemove");
    }
}
