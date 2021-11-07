using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

public class HotfixScripts : MonoBehaviour
{
    //Lua虚拟机
    private LuaEnv luaEnv;
    //定义字典，方便每次AB包资源的加载读取
    public static Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        //Lua虚拟机
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        //执行lua文件,require后面值等同于filePath
        luaEnv.DoString("require'fish'");
    }
    void Start()
    {
       
    }
    private byte[] MyLoader(ref string filePath)
    {
        //导入lua文件
        string absPath = @"D:\XluaProjects\PlayerGamePackage\" + filePath + ".lua.txt";
        //读入lua文件并将返回值转化为Byte数组
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }
    //在OnDestory之前执行的生命周期函数
    private void OnDisable()
    {
        //在虚拟机释放之前将方法置空
        luaEnv.DoString("require'fishDispose'");
    }
    private void OnDestroy()
    {
        //销毁释放Lua虚拟机
        luaEnv.Dispose();
    }
    [LuaCallCSharp]
    public static void LoadResource(string resName,string filePath)
    {
        //读取本地AB包资源
        AssetBundle ab = AssetBundle.LoadFromFile(@"xxx");
        //用GameObject接收AB包资源
        GameObject gameObject = ab.LoadAsset<GameObject>(resName);
        prefabDict.Add(resName, gameObject);
    }
    [LuaCallCSharp]
    //读取游戏物体的方法
    public static GameObject GetGameObject(string goName)
    {
        return prefabDict[goName];
    }
}
