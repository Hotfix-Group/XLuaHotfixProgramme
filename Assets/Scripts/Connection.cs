using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using Protocol;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    public GameObject Msgbox;
    static string localVer="1.0";

    private readonly Dictionary<int, Type> _responseMsgDic = new Dictionary<int, Type>()
    {
        {(int)SERVER_CMD.ServerCheckRsp,typeof(CheckRsp)},
        //目前只会收到更新检查的结果这一个回报
    
    };

    private void Awake()
    {
        NetWorkMgr.Instance.InitConnect();
    }
    private void Start()
    {       
        EventModule.Instance.AddNetEvent((int)SERVER_CMD.ServerCheckRsp, OnCheckRsp);    
    }

    private void Update()
    {
        while (NetWorkMgr.Instance.receiveQueue != null && NetWorkMgr.Instance.receiveQueue.Count > 0)
        {
            Debug.Log("队列中事件执行");
 
            NetMsg mgs = NetWorkMgr.Instance.receiveQueue.Dequeue();
 
            EventModule.Instance.Dispatch(mgs.cmd, mgs.msg);
        }

    }
    //检查更新请求协议
    public void onClickCheck()
    {
        CheckReq req = new CheckReq();
        Debug.Log("当前版本：" +localVer);
        req.LocalVer = localVer;

        NetWorkMgr.Instance.SendMsg((int)CLIENT_CMD.ClientCheckReq, req);
        Debug.Log("已发送检查更新请求协议");
    }
    //检查更新回报协议
    private void OnCheckRsp(int cmd, IMessage msg)
    {
        CheckRsp rsp = (CheckRsp)msg;
        //最新可用版本
        Debug.Log(rsp.NewVer);
        switch (rsp.Result)
        {
            case 0:
                Debug.Log("检测到可用更新,最新版本为："+rsp.NewVer);

                Msgbox.SetActive(true);
                //这两个按钮添加事件的语句我写到另一个类里了, 主场景的entrance物体的脚本里,要不然没法正常添加
                break;
            case -1:
                Debug.Log("已是最新版本！");
                break;
            default:
                Debug.Log("网络错误，请尝试重启游戏");
                break;
        }
    }

}
