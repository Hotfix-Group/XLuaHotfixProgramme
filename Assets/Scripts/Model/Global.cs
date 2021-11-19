using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;
/// <summary>
/// 定义全局的数据模型
/// </summary>

public class GlobalDataProxy : PureMVC.Patterns.Proxy
{
    public new static string NAME = "GlobalDataProxy";
    //定义数据代理模型，参数为数据名和数据实体
    public GlobalDataProxy(string proxyName,object data = null) : base(proxyName, data) 
    { 
    }

    public GlobalData GetGlobalData
    {
        get
        {
            return Data as GlobalData;
        }
    }

    public override void OnRegister()
    {
        base.OnRegister();
        
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
    //序列化信息，游戏保存时使用？
    public void SerializeData()
    {
        //save data
    }
    //解序列化信息？从Json或其他文件读取全局参数
    public void DeserializeData()
    {
        //load data
    }

}
[Serializable]
//全局数据类,这里放UI中显示的数据或者UI的参数
public class GlobalData
{
  
}
