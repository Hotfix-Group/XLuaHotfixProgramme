using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 继承Facade核心类
/// proxy,mediator,command的初始化和获取都在此进行
/// 
/// 执行顺序：
/// Model -> Controller -> View Facade
/// 
/// 每次Facade重写都要调用base.InitializeFacade()
/// </summary>
public class ApplicationFacade : PureMVC.Patterns.Facade
{
    //Notification常量
    public const string START_UP = "start_up";//游戏启动
    public const string PLAY = "play";//游戏开始
    public const string GET_VERSION = "get_version";//获取当前版本
    public const string CHECK_UPDATE = "check_update";//检查更新
    public const string UPDATE_DOWNLOAD = "update_download";//下载更新
    public const string SKILL_ICE = "skill_ice";//冰冻技能
    public const string SKILL_FIRE = "skill_fire";//火焰技能
    public const string SKILL_SG = "skill_sg";//霰弹技能
    public const string COST_DIAMOND = "cost_diamond";//消耗钻石

    static ApplicationFacade()
    {
        m_instance = new ApplicationFacade();
    }
    public static ApplicationFacade GetInstance()
    {
        return m_instance as ApplicationFacade;
    }

    //通过command命令启动游戏
    public void Launch()
    {
        SendNotification(START_UP);
    }
    /// <summary>
    /// Controller初始化，将各类Command注册到Notification完成映射
    /// </summary>
    protected override void InitializeController()
    {
        base.InitializeController();

        RegisterCommand(START_UP, typeof(StartUpCommand));
        RegisterCommand(PLAY, typeof(PlayCommand));
        RegisterCommand(COST_DIAMOND, typeof(CostDiamondCommand));
    }
    /// <summary>
    /// 初始化View，UI初始化在Command中执行
    /// </summary>
    protected override void InitializeView()
    {
        base.InitializeView();
    }

    protected override void InitializeFacade()
    {
        base.InitializeFacade();
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        RegisterProxy(new VersionProxy(VersionProxy.NAME));
        RegisterProxy(new PlayerdataProxy(PlayerdataProxy.NAME));
    }
}
