using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net.Sockets;
using UserInfo;

//数据层
public class RunSocket
{
    /// <summary>
    /// UDPSocket
    /// </summary>
    UDPSocket useSocket;
    /// <summary>
    /// 信息
    /// </summary>
    User userInfo;
    /// <summary>
    /// 临时存储字节流
    /// </summary>
    byte[] tempBytes;

    public static bool isRecv;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="userName"></param>
    public RunSocket(string userName)
    {
        userInfo.name = userName;
        useSocket = new UDPSocket(Recvs, 18001, 1024);
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="IP">目标ip地址</param>
    /// <param name="msg">发送的信息</param>
    public void SendBytes(string IP,string msg)
    {
        userInfo.msg = msg;
        byte[] sendBytes = ProtocolBufferTool.Serialize(userInfo);
        useSocket.SendData(sendBytes, IP, 18001);
    }

    /// <summary>
    /// 当isRecv为true时，接收信息，返回信息
    /// </summary>
    /// <returns>信息</returns>
    public User ConvertFromNet()
    {
        User user = ProtocolBufferTool.Deserialize<User>(tempBytes);
        return user;
    }

    /// <summary>
    /// 委托
    /// </summary>
    /// <param name="buffer">字节流</param>
    /// <param name="recvLen">接收的长度</param>
    /// <param name="port">端口</param>
    /// <param name="Ip">IP</param>
    public void Recvs(byte[] buffer, int recvLen, int port, string Ip)
    {
        isRecv = true;
        tempBytes = new byte[recvLen];
        Buffer.BlockCopy(buffer,0,tempBytes,0,recvLen);
    }

    public void OnMyApplicationQuit()
    {
        useSocket.Dispose();
    }
}

//视图层
public class InputControl : UIBase {
   
    Text ipInput;
    string IP;
	// Use this for initialization
	void Start () {
        
        ipInput = GameObject.FindWithTag("IP").GetComponent<Text>();
    }
	
	void Update () {
    }


}
