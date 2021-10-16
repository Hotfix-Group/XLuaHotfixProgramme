using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Download : MonoBehaviour
{
    public TextAsset assetDatas;
    //不知道要下载的资源名称要怎样获取，先用一个txt保存
    private float loadCount = 0;

    public string url;
    //   public Slider slider;

    public GameObject MsgBox;

   public void OnClickDownload()
    {
        loadCount = 0;
        //获取所有需要下载的资源，用逗号分隔，存为数组
        string[] downloadFiles = assetDatas.text.Split(',');
        //遍历所有需要下载的资源的名字
        foreach (var file in downloadFiles)
        {
            //下载资源
            StartCoroutine(SaveAssetBundle(url, file, () => {
                //资源下载完成后的回调
                loadCount++;
                Debug.Log("下载进度 : " + (loadCount / (float)downloadFiles.Length * 100) + "%");
              //  slider.value = loadCount / (float)downloadFiles.Length;
            }));
        }
    }

    IEnumerator SaveAssetBundle(string path, string filename, Action DownLoad = null)
    {
        //服务器上的文件路径，本地服务器用HFS，腾讯云那边用IIS生成
        string originPath = path + filename;

        using (UnityWebRequest request = UnityWebRequest.Get(originPath))
        {
            yield return request.SendWebRequest();

            //下载完成后执行的回调
            if (request.isDone)
            {
                byte[] results = request.downloadHandler.data;
                string savePath = Application.dataPath + "/Texture";

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                FileInfo fileInfo = new FileInfo(savePath + "/" + filename);
                FileStream fs = fileInfo.Create();
                //fs.Write(字节数组, 开始位置, 数据长度);
                fs.Write(results, 0, results.Length);
                fs.Flush(); //文件写入存储到硬盘
                fs.Close(); //关闭文件流对象
                fs.Dispose(); //销毁文件对象
                if (DownLoad != null)
                    DownLoad();

            }
        }
    }

    public void OnClickCancel()
    {
        MsgBox.SetActive(false);
    }
}
