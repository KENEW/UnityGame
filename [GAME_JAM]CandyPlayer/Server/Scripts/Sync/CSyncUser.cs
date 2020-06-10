using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Sockets;
using System.Net;

using System;
using System.Text;
using System.Runtime.InteropServices;

public enum EUserType : byte
{
    User = 0,
    Boss = 1
}

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FUserInfo
{
    public uint m_userIndex;

    public EUserType m_userType;

    public ushort m_nowHelath;
    public ushort m_maxHelath;

    public FPosition m_position;
    public float m_moveSpeed;
}

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FPosition
{
    public FVector position;
    public FVector direction;

    public FPosition(FVector _direction, float _x = 0.0f, float _y = 0.0f)
    {
        position = new FVector(_x, _y);
        direction = _direction;
    }
}

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct FVector
{
    public float m_x;
    public float m_y;

    public FVector(float _x = 0.0f, float _y = 0.0f)
    {
        m_x = _x;
        m_y = _y;
    }
}

//흠...
public class CSyncUser : CEntity
{
    public FUserInfo m_userInfo;

    public bool bIsMove;
    public bool bIsMoveFinish;

    public float speed;

    private Vector2 targetPosition;
    public int direction;

    public Animator animator;

    protected virtual void Update()
    {
        if (animator != null)
            animator.SetBool("Move", bIsMove);

        if (!bIsMove) return;

        switch (direction)
        {
            case 0:
                this.transform.Translate(Vector2.up * speed * Time.deltaTime);
                break;
            case 90:
                this.transform.Translate(Vector2.right * speed * Time.deltaTime);
                break;
            case 180:
                this.transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;
            case 270:
                this.transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;
            case 45:
                this.transform.Translate((Vector2.up + Vector2.left) * speed * Time.deltaTime);
                break;
            case 135:
                this.transform.Translate((Vector2.down + Vector2.right) * speed * Time.deltaTime);
                break;
            case 225:
                this.transform.Translate((Vector2.down + Vector2.left) * speed * Time.deltaTime);
                break;
            case 315:
                this.transform.Translate((Vector2.up + Vector2.right) * speed * Time.deltaTime);
                break;
            case 360:
                this.transform.Translate(Vector2.up * speed * Time.deltaTime);
                break;
        }
        // this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
    }
    public override void Hit()
    {
        base.Hit();
        Freeze();
    }

    public override void Freeze()
    {
        base.Freeze();
        animator.SetTrigger("Freeze");
    }

    public override void Death()
    {
        base.Death();
        animator.SetTrigger("Death");
    }


    public void SetUserInfo(FUserInfo _userInfo)
    {
        m_userInfo = _userInfo;
        speed = m_userInfo.m_moveSpeed;

        this.transform.position = new Vector3(
            m_userInfo.m_position.position.m_x,
            m_userInfo.m_position.position.m_y);
    }

    public void BeginMove(FVector _position, int _direction)
    {
        Vector2 compareVector = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 pathPosition = new Vector2(_position.m_x, _position.m_y);

        if (Vector2.SqrMagnitude(compareVector - pathPosition) > 10.0f * 10.0f)
        {
            this.transform.position = pathPosition;
            return;
        }


        _direction %= 360;
        direction = _direction;

        //말도 안되는 소스
        switch (_direction)
        {
            case 0: targetPosition = Vector2.up * speed * 1.5f + compareVector;
                break;
            case 90: targetPosition = Vector2.right * speed  * 1.5f+ compareVector;
                break;
            case 180:
                targetPosition = Vector2.down * speed* 1.5f + compareVector ;
                break;
            case 270:
                    targetPosition = Vector2.left * speed * 1.5f + compareVector;
                
                break;
            case 45:
                {
                    targetPosition = Vector2.up * speed * 1.5f;
                    targetPosition += Vector2.left * speed * 1.5f;
                    targetPosition += compareVector;
                   // targetPosition = (Vector2.up + Vector2.left) * speed + compareVector * 1.5f;
                }
                break;
            case 135:
                {
                    targetPosition = Vector2.down * speed * 1.5f;
                    targetPosition += Vector2.right * speed * 1.5f;
                    targetPosition += compareVector;
                //    targetPosition = (Vector2.down + Vector2.right) * speed * 1.5f + compareVector;
                }
                break;
            case 225:
                {
                    targetPosition = Vector2.down * speed * 1.5f;
                    targetPosition += Vector2.left * speed * 1.5f;
                    targetPosition += compareVector;
                   // targetPosition = (Vector2.down + Vector2.left) * speed * 1.5f + compareVector;
                }
                break;
            case 315:
                {
                    targetPosition = Vector2.up * speed * 1.5f;
                    targetPosition += Vector2.right * speed * 1.5f;
                    targetPosition += compareVector;
                    //targetPosition = (Vector2.up + Vector2.right) * speed * 1.5f + compareVector;
                }
                break;
            case 360:
                targetPosition = Vector2.up * speed  * 1.5f+ compareVector;
                break;
        }

        bIsMove = true;
    }

    public void EndMove(FVector _position, int _direction)
    {
        bIsMove = false;

        this.transform.position = new Vector2(_position.m_x, _position.m_y);
    }

}
