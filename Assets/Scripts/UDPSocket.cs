using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSocket
{
    Socket mySocket;
    byte[] recvData;
    Thread tempThread;
    public delegate void RecvDelegate(byte[] buffer, int recvLen, int port, string Ip);
    RecvDelegate recvDelegate;
    public bool isRuningThread;

    public UDPSocket(RecvDelegate recv, int port, int recvLen)
    {
        recvDelegate = recv;
        Initial(port, recvLen);
    }

    public void Initial(int port, int recvLength)
    {
        recvData = new byte[recvLength];
        mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        mySocket.Bind(endPoint);
        tempThread = new Thread(RecvThread);
        tempThread.Start();
        isRuningThread = true;
    }

    public void SendData(byte[] buffer, string ip, int port)
    {
        IPEndPoint sendIp = new IPEndPoint(IPAddress.Parse(ip), port);
        mySocket.SendTo(buffer, sendIp);
    }

    public void RecvThread()
    {
        while (isRuningThread)
        {
            if (mySocket == null || mySocket.Available < 1)
            {
                Thread.Sleep(100);
            }
            Recv();
        }
    }

    public void Dispose()
    {
        isRuningThread = false;
        if(tempThread!=null)
        tempThread.Abort();
        mySocket.Close();
    }

    public void Recv()
    {
        IPEndPoint recvIp = new IPEndPoint(IPAddress.Any, 0);
        EndPoint tempRecvIp = (EndPoint)recvIp;
        //阻塞式
        int realRecv = mySocket.ReceiveFrom(recvData, ref tempRecvIp);
        if (recvDelegate != null)
        {
            recvDelegate(recvData, realRecv, recvIp.Port, recvIp.Address.ToString());
        }
    }
}