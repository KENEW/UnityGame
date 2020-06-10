using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using System.Net.Sockets;
using System.Net;
using System.Threading;

using System;
using System.Text;
using System.Runtime.InteropServices;

[System.Serializable]
public class CSession
{
    public Socket m_socket;
    private static ManualResetEvent g_connectDone =
        new ManualResetEvent(false);

    public byte[] m_recvBuffer = new byte[10000];
    public byte[] m_packetBuffer = new byte[10000];

    protected object m_lockObject = new object();

    private int m_packetReadPoint;

    private CPacket m_basePacket;
    
    public bool m_bIsConnect;

    public void Init()
    {
        m_socket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);
    }

    public virtual void Update()
    {
        ReceiveLoop();
    }

    protected void ReceiveLoop()
    {
        if (m_packetReadPoint >= 4)
        {
            lock (m_lockObject)
            {
                unsafe
                {
                    fixed (byte* fixed_buffer = m_packetBuffer)
                    {
                        Marshal.PtrToStructure((IntPtr)fixed_buffer, m_basePacket);
                    }
                }

                if (m_basePacket.size <= m_packetReadPoint)
                {
                    try
                    {
                        ProcessCommand(m_basePacket, m_packetBuffer);

                        Buffer.BlockCopy(
                            m_packetBuffer,
                            m_basePacket.size,
                            m_packetBuffer,
                            0,
                            m_packetReadPoint - m_basePacket.size);

                        m_packetReadPoint -= m_basePacket.size;
                    }
                    catch (SocketException SCE)
                    {
                        Debug.Log("Socket connect error! : " + SCE.ToString());
                    }
                }
            }
        }
    }
    public virtual bool Connect(string _ip, int _port)
    {
        try
        {
            m_socket.BeginConnect(_ip, _port, new AsyncCallback(ConnectCallback), m_socket);
            if (g_connectDone.WaitOne(1000))
                BeginReceive();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        return true;
    }

    protected static void ConnectCallback(IAsyncResult ar)
    {
        Socket socket = (Socket)ar.AsyncState;

        try
        {
            socket.EndConnect(ar);
            g_connectDone.Set();

            //ServerManager.Instance.BeginReceive();
            //ServerManager.Instance.ConnectSuccess();
        }
        catch (Exception e)
        {
            socket.Close();
            g_connectDone.Set();

            Console.WriteLine(e.ToString());

            //ServerManager.Instance.Disconnect();
        }
    }

    public void SendData(byte[] _buffer)
    {
        m_socket.Send(_buffer);
    }

    public void SendData(byte[] _buffer, int _size)
    {
        m_socket.Send(_buffer, _size, SocketFlags.None);
    }

    public void BeginReceive()
    {
        m_basePacket = new CPacket();

        m_socket.BeginReceive(m_recvBuffer, 0,
            m_recvBuffer.Length, SocketFlags.None,
            new AsyncCallback(ReciveCallback), m_socket);

        m_packetReadPoint = 0;
    }

    private void ReciveCallback(IAsyncResult ar)
    {
        try
        {
            int recvSize = m_socket.EndReceive(ar);

            lock (m_lockObject)
            {
                Buffer.BlockCopy(m_recvBuffer, 0, m_packetBuffer, m_packetReadPoint, recvSize);
                m_packetReadPoint += recvSize;
            }

            m_socket.BeginReceive(m_recvBuffer, 0,
                m_recvBuffer.Length, SocketFlags.None,
                new AsyncCallback(ReciveCallback), m_socket);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());

            m_bIsConnect = false;
        }
    }

    public virtual void ProcessCommand(CPacket _packet, byte[] _buffer)
    {
    }

    public bool isSocketConnected()
    {
        //처음 타이틀 화면에서 wsad누르면 오류발생
        //서버매니저에서 한번 거쳐야할듯
        return m_socket.Connected;
    }
}
