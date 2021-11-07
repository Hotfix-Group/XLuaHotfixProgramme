using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Protocol;
using System.Threading;
using UnityEngine;
/// <summary>
/// 不挂在物体上,用于管理socket链接的单例类，客户端的一切发送接收, 所有的跟收发有关的代码都在这里定义,在别处调用
/// 不用单例会导致切换场景时物体丢失而连接随之丢失
/// </summary>

public struct NetMsg
{
    public int cmd;
    public IMessage msg;
}
public class NetWorkMgr
{
    public bool hasLogin = false;
    private static NetWorkMgr _Instance;

    public static NetWorkMgr Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new NetWorkMgr();
            return _Instance;
        }
    }



    public Queue<NetMsg> receiveQueue; //服务器消息接收队列

    public string staInfo = "NULL";             //状态信息
    public string ip = "119.29.165.241";   //输入ip地址
    public string port = "8086";           //输入端口号
    private int _recTimes = 0;                    //接收到信息的次数
    private string _recMes = "NULL";              //接收到的消息
    public Socket _socketSend;                   //客户端套接字，用来链接远端服务器

    private byte[] _headBytes;

    //将协议号与返回的消息类型对应上
    private readonly Dictionary<int, Type> _responseMsgDic = new Dictionary<int, Type>()
    {
      {(int)SERVER_CMD.ServerCheckRsp,typeof(CheckRsp)},
    };

    public void InitConnect()
    {
        if (_headBytes == null)
        {
            char[] head = new[] { 'T', 'C' };
            _headBytes = Encoding.Default.GetBytes(head);
        }

        ConnectToServer();

    }

    //建立链接
    private void ConnectToServer()
    {
        try
        {
            int _port = Convert.ToInt32(port);             //获取端口号
            string _ip = this.ip;                               //获取ip地址

            //创建客户端Socket，获得远程ip和端口号
            _socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint point = new IPEndPoint(ip, _port);

            _socketSend.Connect(point);
            Debug.Log("连接成功 , " + " ip = " + ip + " port = " + _port);
            staInfo = ip + ":" + _port + "  连接成功";

            receiveQueue = new Queue<NetMsg>();
            //开启新的线程，不停的接收服务器发来的消息，不能放在Update里面接收，会卡死主线程
            Thread r_thread = new Thread(Received);      
            r_thread.IsBackground = true;
            r_thread.Start();     

        }
        catch (Exception)
        {
            Debug.Log("IP或者端口号错误......");
            staInfo = "IP或者端口号错误......";
        }
    }

    void Received()
    {
        while (true)
        {
            try
            {
                byte[] buffer = new byte[60240];
                //实际接收到的有效字节数
                int len = _socketSend.Receive(buffer);
                if (len == 0)
                {
                    break;
                }
                //解码 , 然后入队 , 然后在update里执行
                EnqueueMsg(buffer);
            }
            catch { }
        }
    }

    public static Int16 bytesToInt16(byte[] src, int offset)
    {
        Int16 value;
        value = (Int16)((src[offset] & 0xFF)
                         | ((src[offset + 1] & 0xFF) << 8));
        return value;
    }

    public void EnqueueMsg(byte[] buffer)
    {
        byte[] headBytes = new byte[2];
        byte[] lengthBytes = new byte[2];
        byte[] cmdBytes = new byte[2];

        Buffer.BlockCopy(buffer, 0, headBytes, 0, 2);
        if (headBytes[0] == this._headBytes[0] && headBytes[1] == this._headBytes[1])
        {
            Buffer.BlockCopy(buffer, 2, lengthBytes, 0, 2);
            int length = bytesToInt16(lengthBytes, 0);
            Buffer.BlockCopy(buffer, 4, cmdBytes, 0, 2);
            int cmd = bytesToInt16(cmdBytes, 0);

            byte[] body = new byte[length - 2];
            Buffer.BlockCopy(buffer, 6, body, 0, body.Length);

            Type tp;
            if (_responseMsgDic.TryGetValue(cmd, out tp))
            {
                //需要动态的创建一个实例的时候，就用Activator.CreateInstance(Type type); 如果是明确的知道要创建哪个实例的模型，就可以用 new
                IMessage msg = (IMessage)Activator.CreateInstance(tp);
                msg.MergeFrom(body);
                NetMsg netMsg;
                netMsg.cmd = cmd;
                netMsg.msg = msg;
                //入队
                receiveQueue.Enqueue(netMsg); //不能直接处理消息，要放到主线程处理消息，在C#线程无法使用Unity对象
            }
        }
    }

  

    public void SendMsg(int cmd, IMessage msg)
    {
        byte[] body = msg.ToByteArray();

        Int16 length = (Int16)(body.Length + 2);
        byte[] lengthByte = BitConverter.GetBytes(length);

        byte[] cmdByte = BitConverter.GetBytes((Int16)cmd);

        int packageLength = 4 + length;
        byte[] package = new byte[packageLength];
        Buffer.BlockCopy(_headBytes, 0, package, 0, _headBytes.Length);
        Buffer.BlockCopy(lengthByte, 0, package, 2, lengthByte.Length);
        Buffer.BlockCopy(cmdByte, 0, package, 4, cmdByte.Length);
        Buffer.BlockCopy(body, 0, package, 6, body.Length);

        _socketSend.Send(package);

    }

}