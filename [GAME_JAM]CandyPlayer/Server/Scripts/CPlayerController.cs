using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerController : CEntity
{
    public FUserInfo m_userInfo;

    [SerializeField]
    private bool isMove;
    public bool IsMove
    {
        get { return isMove; }
        set
        {
            if (isMove == value) return;

            isMove = value;

            SendPlayerPosition();

            sendTime = 0.01f;
        }
    }
    protected float sendTime;
    public float sendInterval = 0.2f;

    protected SpriteRenderer spriteRenderer;
    
    public virtual void SetUserInfo(FUserInfo _userInfo)
    {
        m_userInfo = _userInfo;
        //posX = m_userInfo.m_position.position.m_x;
        //posY = m_userInfo.m_position.position.m_y;
        this.transform.position =
            new Vector2(m_userInfo.m_position.position.m_x,
            m_userInfo.m_position.position.m_y);
        speed = m_userInfo.m_moveSpeed;
    }

    public uint GetUserIndex()
    {
        return m_userInfo.m_userIndex;
    }

    [SerializeField]
    private int direction;
    private bool isup, isdown, isleft, isright;
    
    public float speed = 1;

    public bool wallCheck;

    public Animator animator;


    protected virtual void Start()
    {
        wallCheck = false;

        CCameraFollow.instance.followTarget = this.transform;
    }

    public override void Hit()
    {
        base.Hit();
        Freeze();        
    }

    public override void Freeze()
    {
        if (isFreeze) return;

        base.Freeze();
        animator.SetTrigger("Freeze");
    }

    public override void Death()
    {
        base.Death();
        animator.SetTrigger("Death");
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "wall")
        {
            wallCheck = true;
            Debug.Log("true");
        }
        if (coll.gameObject.tag == "item")
        {
            Destroy(coll.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "wall")
            wallCheck = false;
    }

    protected virtual void Update()
    {
        if (isFreeze) return;

        if (isFreezeTime)
        {
            freezeTime += Time.deltaTime;

            if (freezeTime >= deathTime)
                Death();
        }

        InputMove();
        if (isright || isleft || isup || isdown) IsMove = true;
        else IsMove = false;

        if (animator != null) 
        animator.SetBool("Move", IsMove);

        if (IsMove)
            MoveProcess();
       
        SendPositionUpdate();
    }

    private void InputMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!isright && !isleft)
            {
                isleft = true;
                SendPlayerPosition();
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isleft = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!isleft && !isright)
            {
                isright = true;
                SendPlayerPosition();
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isright = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (!isdown && !isup)
            {
                isup = true;
                SendPlayerPosition();
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isup = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!isup && !isdown)
            {
                isdown = true;
                SendPlayerPosition();
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isdown = false;
        }

        direction %= 360;
    }
    private void MoveProcess()
    {
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
    }

    private void SendPlayerPosition()
    {
        FVector position = new FVector(this.transform.position.x, this.transform.position.y);
        if (isup)
        {
            direction = 360;
            if (isleft) direction += 45;
            if (isright) direction -= 45;
        }
        else if (isdown)
        {
            direction = 180;
            if (isleft) direction += 45;
            if (isright) direction -= 45;
        }
        else if (isleft)
            direction = 270;
        else if (isright)
            direction = 90;

        CUserPositionPacket positionPacket;

        if (isMove)
        {
            positionPacket = new CUserPositionPacket(
                ESendHeader.Move, GetUserIndex(), position, direction);
        }
        else
        {
            positionPacket = new CUserPositionPacket(
                ESendHeader.MoveFinish, GetUserIndex(), position, direction);
        }
        CServerManager.Instance.SendData(positionPacket.GetByte());
    }
    private void SendPositionUpdate()
    {
        if (IsMove)
        {
            sendTime += Time.deltaTime;

            if (sendTime >= sendInterval)
            {
                SendPlayerPosition();
                sendTime = 0.0f;
            }
        }
    }

}
