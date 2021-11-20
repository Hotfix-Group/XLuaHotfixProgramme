using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 调用luaManager启动lua脚本初始化游戏
/// </summary>

public class GameInit : MonoSingleton<GameInit>
{

    public delegate void LifeCircleCallBack();

    public event LifeCircleCallBack onUpdate = null;
    public event LifeCircleCallBack onFixedUpdate = null;
    public event LifeCircleCallBack onLateUpdate = null;
    public event LifeCircleCallBack onGUI = null;
    public event LifeCircleCallBack onDestroy = null;
    public event LifeCircleCallBack onApplicationQuit = null;


    void onAwake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        Application.targetFrameRate = 60;        
        //启动Lua管理器
        LuaManager.GetInstance().StartUp();
     
      

    }

    private void Start()
    {
        //DoString
        LuaManager.GetInstance().OnEntry();
        //启动MVC
        ApplicationFacade.GetInstance().Launch();

    }

     void Update()
    {
        if (this.onUpdate != null)
            this.onUpdate();
    }

    private void FixedUpdate()
    {
        if (this.onFixedUpdate != null)
            this.onFixedUpdate();
    }
    private void LateUpdate()
    {
        if (this.onLateUpdate != null)
            this.onLateUpdate();
    }
    void OnApplicationQuit()
    {
        if (this.onApplicationQuit != null)
            this.onApplicationQuit();
    }

    void OnGUI()
    {
        if (this.onGUI != null)
            this.onGUI();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (this.onDestroy != null)
            this.onDestroy();
    }

}
