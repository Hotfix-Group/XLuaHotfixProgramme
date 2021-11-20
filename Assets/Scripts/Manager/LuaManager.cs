using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using System;

public class LuaManager : MonoSingleton<LuaManager>
{
    //存放lua代码的文件夹名，不需要
    public const string LuaScriptsFolder = "LuaScripts";
    //整个程序仅doSting1个main脚本,用该脚本require其他脚本
    const string luaGameEntryScript = "Main";

    private LuaEnv luaEnv = null;
    private LuaTable luaTable = null;
    private LuaTable meta = null;
    //储存lua文件地址
    private Dictionary<string, string> luaFilePathDict = new Dictionary<string, string>();
    // Start is called before the first frame update
    //void Start(){}
    protected override void Init()
    {
        base.Init();
        InitLuaEnv();
    }
    //初始化Lua虚拟机
    private void InitLuaEnv()
    {
        luaEnv = new LuaEnv();
        if (luaEnv != null)
        {
            luaEnv.AddLoader(CustomLoader);
            this.meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
        }
        //储存lua文件的字典，属于C#加载lua，这里不需要
        //luaFilePathDict.Add("1","test/1");
        //luaFilePathDict.Add("2", "test/2");
        //luaFilePathDict.Add("3", "test/3");
      
    }
    //自定义的loader,要注意根据平台变更路径，返回值是lua文件内容
    public static byte[] CustomLoader(ref string _filePath)
    {
        string _scriptPath = string.Empty;
        _filePath = _filePath.Replace(".", "/") + ".lua.txt";
        //多平台路径的判断，这里只写了windows
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            _scriptPath = Path.Combine(Application.dataPath, LuaScriptsFolder);
            _scriptPath = Path.Combine(_scriptPath, _filePath);
            byte[] _bytes = FileManager.SafeReadAllBytes(_scriptPath);
            return _bytes;
        }
        return null;
    }
    private void loadScript(string _scriptName)
    {
        DoString(string.Format("require('{0}')", _scriptName));       
    }

    //Lua脚本启动入口，启动main.lua脚本
    public void OnEntry()
    {
        if (luaEnv != null)
        {
            loadScript(luaGameEntryScript);
            DoString("Main.Startup()");
        }
    }
    public void GameStart()
    {
        if (luaEnv != null)
        {
            DoString("Main.EnterGame()");
        }
    }
    //执行lua脚本
    public void DoString(string _luaScript,string _chunkName="chunk",LuaTable _luaTable = null)
    {
        if (luaEnv != null)
        {
            try
            {
                luaEnv.DoString(_luaScript,_chunkName,_luaTable);
                Debug.Log("Do String");
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
    //绑定luabehaviour
    public LuaTable InitMonoBehaviour(LuaBehaviour luaBehaviour, string scriptName)
    {
        Debug.Log("InitMonoBehaviour");
        //新建一个表，并设置元表为上面定义的meta
        luaTable = luaEnv.NewTable();
        luaTable.SetMetaTable(meta);
        //把xLuaMonoBehaviour对象传到lua侧
        luaTable.Set("self", luaBehaviour);
        //启动luamono，我们选择在main里面require,不需要这里
        DoString(LoadLuaScript("LuaMonoBehaviour"), "LuaMonoBehaviour", luaTable);
        //加载字典储存的lua脚本，如果在main里面已经require了则不需要此方法
        //DoString(LoadLuaScript(luaFilePathDict[scriptName]), scriptName, luaTable);
        return luaTable;
    }
    //用C#读取lua文件的方法，我们不需要
    private string LoadLuaScript(string _filePath)
    {
        Debug.Log("LoadLuaScript" + _filePath);
        string _scriptPath = string.Empty;
        _filePath = _filePath.Replace(".", "/") + ".lua.txt";
        _scriptPath = Path.Combine(Application.dataPath, LuaScriptsFolder);
        _scriptPath = Path.Combine(_scriptPath, _filePath);
        string str = FileManager.GetFileContent(_scriptPath);
        return str;
    }
    //用委托桥接C#和lua的方法
    public Action CallFunction(string _targetName,string _function)
    {
        return luaTable.Get<Action>(_function);
    }

}
