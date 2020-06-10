using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using System.Net.Sockets;
using System.Net;
using System.Threading;

using System;
using System.Text;
using System.Runtime.InteropServices;

public class CPlayerSession : CSession
{
    public uint userIndex;

    public override void ProcessCommand(CPacket _packet, byte[] _buffer)
    {
        switch ((EReciveHeader)_packet.GetCommand())
        {
            case EReciveHeader.InitUser:
                {
                    CUserIndexPacket indexPacket = new CUserIndexPacket();
                    ParsingPacket(_buffer, ref indexPacket);

                    userIndex = indexPacket.userIndex;

                   // CPlayerController.instance.m_userInfo.m_userIndex = userIndex;

                    Debug.Log(userIndex);
                }
                break;

            case EReciveHeader.GameStart:
                {
                    CUserInfoListPacket userListPacket = new CUserInfoListPacket();
                    ParsingPacket(_buffer, ref userListPacket);

                    CServerManager.Instance.InitGameUser(userListPacket);
                }
                break;

            case EReciveHeader.Move:
                {
                    CUserPositionPacket positionPacket = new CUserPositionPacket();
                    ParsingPacket(_buffer, ref positionPacket);

                    CServerManager.Instance.MoveUser(positionPacket);
                }
                break;

            case EReciveHeader.MoveFinish:
                {
                    CUserPositionPacket positionPacket = new CUserPositionPacket();
                    ParsingPacket(_buffer, ref positionPacket);

                    CServerManager.Instance.MoveFinishUser(positionPacket);
                }
                break;

            case EReciveHeader.BossAttack:
                {
                    CUserIndexPacket packet = new CUserIndexPacket();
                    ParsingPacket(_buffer, ref packet);

                    CServerManager.Instance.AttackBoss(packet);
                }
                break;
        }
    }

    private void ParsingPacket<T>(byte[] _buffer, ref T packet)
    {
        unsafe
        {
            fixed (byte* fixed_buffer = _buffer)
            {
                Marshal.PtrToStructure((IntPtr)fixed_buffer, packet);
            }
        }
    }
}
