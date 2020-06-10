using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossController : CPlayerController
{
    public CircleCollider2D AttackLine;
    public Animator attackAnimator;

    protected override void Start()
    {
        base.Start();
        AttackLine.enabled = false;
    }

    protected override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackLine.enabled = true;

            CUserIndexPacket packet = new CUserIndexPacket(ESendHeader.BossAttack,
                CServerManager.Instance.m_session.userIndex);
            CServerManager.Instance.SendData(packet.GetByte());
        }

        base.Update();
    }

    public override void SetUserInfo(FUserInfo _userInfo)
    {
        base.SetUserInfo(_userInfo);
    }

    public override void Attack()
    {
        attackAnimator.SetTrigger("BossAttack");
    }
}
