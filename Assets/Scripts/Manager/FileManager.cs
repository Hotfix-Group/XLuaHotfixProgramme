using UnityEngine;
using System.IO;
using System;

using XLua;
/// <summary>
/// 
/// </summary>
public class FileManager
{
    //读取Lua文件内容的方法
    public static byte[] SafeReadAllBytes(string inFile)
    {
        try
        {
            if (string.IsNullOrEmpty(inFile))
            {
                return null;
            }

            if (!File.Exists(inFile))
            {
                return null;
            }

            File.SetAttributes(inFile, FileAttributes.Normal);
            return File.ReadAllBytes(inFile);
        }
        catch (System.Exception ex)
        {

            return null;
        }
    }
    //读取文件内容
    public static string GetFileContent(string filepath)
    {
        string errorStr = "";
        try
        {
            FileInfo fileInfo = new FileInfo(filepath);
            if (!fileInfo.Exists)
            {
                return "The File not exists";
            }
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                string ret = System.Text.Encoding.UTF8.GetString(bytes);
                return ret;
            }
        }
        catch (Exception ex)
        {
            errorStr = ex.Message;
            Debug.Log(ex.Message);
        }
        return errorStr;
    }

}
