using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSyncBoss : CSyncUser
{
    public CircleCollider2D AttackLine;
    public Animator attackAnimator;

    private void Awake()
    {
        AttackLine.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        AttackLine.enabled = true;
        attackAnimator.SetTrigger("BossAttack");
    }
}
