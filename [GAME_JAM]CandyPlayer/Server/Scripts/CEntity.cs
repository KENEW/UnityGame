using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntity : MonoBehaviour
{
    protected bool isFreeze;
    protected bool isFreezeTime;
    protected bool isDeath;
    protected float freezeTime;
    protected float deathTime = 30.0f;

    public CHitColorChanger colorChanger;

    public virtual void Hit()
    {
        colorChanger.Hit();
    }

    public virtual void Freeze()
    {
        isFreeze = true;
        isFreezeTime = true;
    }

    public virtual void Death()
    {
        isDeath = true;
    }

    public virtual void Revival()
    {
        isFreezeTime = false;
    }

    public virtual void Respawn()
    {
        isFreeze = false;
        freezeTime = 0.0f;
    }

    public virtual void Attack()
    {
    }
}
