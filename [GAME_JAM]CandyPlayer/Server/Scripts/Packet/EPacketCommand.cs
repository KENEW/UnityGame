using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EReciveHeader : ushort
{
    InitUser = 1,
    GameStart = 101,
    Move = 300,
    MoveFinish = 301,

    BossAttack = 1000,
    SpawnCandy = 1500,

    ExitUser = 50000
}

public enum ESendHeader : ushort
{
    LoadingSuccess = 100,
    Move = 300,
    MoveFinish = 301,

    BossAttack = 1000,

    SpawnCandy = 1500,

    ExitUser = 50000
}