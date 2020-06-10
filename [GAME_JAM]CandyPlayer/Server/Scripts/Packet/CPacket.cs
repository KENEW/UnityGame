using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Runtime.InteropServices;

//패킷의 헤더부분
//패킷의 사이즈와 커맨드다.
#region Original Packet
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class CPacket
{
    public ushort size;
    public ushort command;

    public CPacket()
    {
        size = (ushort)Marshal.SizeOf(this);
    }

    public CPacket(ESendHeader _command)
    {
        size = (ushort)Marshal.SizeOf(this);
        command = (ushort)_command;
    }

    public virtual CPacket SetCommand(ESendHeader _command)
    {
        command = (ushort)_command;

        return this;
    }

    public virtual ushort GetCommand()
    {
        return command;
    }

    public virtual ushort GetSize()
    {
        return size;
    }

    public virtual byte[] GetByte()
    {
        byte[] buffer = new byte[size];

        unsafe
        {
            fixed (byte* fixed_buffer = buffer)
            {
                Marshal.StructureToPtr(this, (IntPtr)fixed_buffer, false);
            }
        }

        return buffer;
    }
}


[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class CUserIndexPacket : CPacket
{
    public uint userIndex;

    public CUserIndexPacket() { }
    public CUserIndexPacket(ESendHeader _command, uint _index)
        : base(_command)
    {
        userIndex = _index;

        size = (ushort)Marshal.SizeOf(this);
    }

    public override byte[] GetByte()
    {
        return base.GetByte();
    }
}

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class CUserAttackPacket : CPacket
{
    public uint userIndex;

    public uint skillIndex;
}

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class CUserInfoListPacket : CPacket
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public FUserInfo[] userInfoBUffer;
    int offset;
}

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class CUserPositionPacket : CPacket
{
    public uint userIndex;

    public FVector position;
    public int direction;

    public CUserPositionPacket() { }
    public CUserPositionPacket(ESendHeader _command, uint _index, FVector _position, int _direction)
        : base(_command)
    {
        userIndex = _index;
        position = _position;
        direction = _direction;

        size = (ushort)Marshal.SizeOf(this);
    }

    public override byte[] GetByte()
    {
        return base.GetByte();
    }
}

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class CCandySpawnPacket : CPacket
{
    public uint userIndex;

    public FVector position;

    public CCandySpawnPacket() { }
    public CCandySpawnPacket(ESendHeader _command, uint _index, FVector _position)
        : base(_command)
    {
        userIndex = _index;
        position = _position;

        size = (ushort)Marshal.SizeOf(this);
    }

    public override byte[] GetByte()
    {
        return base.GetByte();
    }
}


#endregion