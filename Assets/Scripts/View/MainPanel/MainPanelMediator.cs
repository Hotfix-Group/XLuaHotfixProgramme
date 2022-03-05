using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
/// <summary>
/// 主页面的Mediator
/// </summary>

public class MainPanelMediator : PureMVC.Patterns.Mediator
{
    public const string MainPanelMediatorName = "MainPanelMediator";

    private MainPanel view;

    VersionProxy versionData;

    public MainPanelMediator (object viewComponent): base(NAME, viewComponent)
    {
        //此处应检查View是否初始化，才可以进行事件绑定
        GameObject obj = GameObject.Find("Canvas/UI_Main");
        if(obj == null)
        {
            Debug.Log("cannot find view,init error");
        }
        view = ((GameObject)ViewComponent).GetComponent<MainPanel>();
        Debug.Log("Main panel mediator");
        versionData = Facade.RetrieveProxy(VersionProxy.NAME) as VersionProxy;
        
        view.BtnPlay.onClick.AddListener(OnClickPlay);
    }
    //点击开始按钮，发送消息
    public void OnClickPlay()
    {
        Debug.Log("Game Start");
        SendNotification(ApplicationFacade.PLAY);
    }
    //预留监听信息的接口，比如检查更新
    public override void HandleNotification (INotification notification)
    {
        switch (notification.Name)
        {
            case ApplicationFacade.CHECK_UPDATE:
                Debug.Log("Check update");
                break;
        }
    }

    public override void OnRegister()
    {

    }
    public override void OnRemove()
    {

    }
}
