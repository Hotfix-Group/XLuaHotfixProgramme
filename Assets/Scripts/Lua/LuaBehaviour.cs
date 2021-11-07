using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using System.IO;
/// <summary>
/// 模拟monobehaviour，实现lua脚本模拟生命周期，并加载lua脚本
/// </summary>

[System.Serializable]
//用来注入unityobj所定义的键值对
public class Injection
{
    public string name;
    public GameObject value;
}
[LuaCallCSharp]
public class LuaBehaviour : MonoBehaviour
{

    //LUA脚本文件，可在unity面板里面赋值
    //public TextAsset LuaScript;
    public string scriptName = "";
    public Injection[] Injections;

    //所有lua脚本共用一个虚拟机
    internal static LuaEnv luaEnv = new LuaEnv(); 
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 

    //lua脚本模拟unity生命周期
    private Action luaAwake;
    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnEnable;
    private Action luaOnDestroy;

    private LuaTable luaTable;

    private void Awake()
    {
        ////获取luaTable，这里会做排序、共用虚拟机等系列操作
        //luaTable = luaEnv.NewTable();
        //// 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        //LuaTable meta = luaEnv.NewTable();
        ////Xlua的底层，获取luaTable所属的虚拟机环境时用的key一定是“__index”
        //meta.Set( "__index", luaEnv.Global);
        ////对有虚拟机和全局环境的table进行绑定
        //luaTable.SetMetaTable(meta);
        ////释放掉传值完成的table
        //meta.Dispose();
        ////绑定C#脚本到luaTable，与上面同理
        //luaTable.Set("self", this);
        ////逐一绑定注入对象列表里的成员
        //foreach(var injection in Injections)
        //{
        //    luaTable.Set(injection.name, injection.value);
        //}
        ////执行lua语句，3个参数分别为lua代码、lua代码在C#中的定义、lua代码在虚拟机里的定义
        //luaEnv.DoString(LuaScript.text, "LuaBehaviour", luaTable);

        this.luaTable = LuaManager.GetInstance().InitMonoBehaviour(this,scriptName);
        //绑定各个生命周期到C#，进行生命周期的模拟
        Action luaAwake = luaTable.Get<Action>("awake");
        luaAwake = CallLuaFunction("Awake");
        luaStart = CallLuaFunction("Start");
        luaUpdate = CallLuaFunction("Update");
        luaOnEnable = CallLuaFunction("OnEnable");
        luaOnDestroy = CallLuaFunction("OnDestroy");
        if (luaAwake != null)
        {
            luaAwake?.Invoke();
        }
    }

    private Action CallLuaFunction(string _fun)
    {
        return LuaManager.GetInstance().CallFunction(gameObject.name,_fun);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (luaStart != null)
        {
            luaStart?.Invoke();
        }
    }

    private void OnEnable()
    {
        luaOnEnable?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if(luaUpdate != null)
        {
            luaUpdate();
        }
        //对update周期进行模拟
        if(Time.time - LuaBehaviour.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaBehaviour.lastGCTime = Time.time;
        }
    }

    private void OnDestroy()
    {
        luaOnDestroy?.Invoke();
        luaOnDestroy = null;
        luaStart = null;
        luaUpdate = null;
        this.luaTable.Dispose();
    }
}
