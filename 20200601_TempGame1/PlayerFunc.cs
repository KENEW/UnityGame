using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunc : MonoBehaviour
{
    //public GameObject dirObj;
    public GameObject bullet;

    public GameObject dirObj;
    public GameObject ShieldObj;

    private ResourceManage resource;
    public GameOfficial gameofficial;

    public Rigidbody2D rb;

    private Vector3 mousePos, mouseDistance, dir;

    private float angle;
    public int mode;
    public float AttackSpeed;
    private bool bulletCheck;

    private void variableInit()
    {
        mode = 1;
        AttackSpeed = 1.0f;
        bulletCheck = true;
    }

    private void Start()
    {
        dirObj = GameObject.Find("DirObj");

        resource = ResourceManage.instanceRes;
        gameofficial = GameOfficial.InstanceOff;

        variableInit();
        objActiveManage(null);
    }

    private void directionFunc()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePos.y - dirObj.transform.position.y, mousePos.x - dirObj.transform.position.x) * Mathf.Rad2Deg;

        dirObj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    private void objActiveManage(GameObject ob)
    {       
        dirObj.SetActive(false);
        ShieldObj.SetActive(false);

        if (ob != null)
            ob.SetActive(true);
    }
    private void mouseCheck()
    {
        if (Input.GetMouseButton(0))
        {
            directionFunc();

            mouseDistance = this.transform.position - mousePos;
            dir = mouseDistance.normalized;
            if (mode == 1)
            {
                objActiveManage(dirObj);
            }
            if (mode == 2)
            {
                gunAttack();
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            objActiveManage(null);
        }
    }

    private void keyBoardCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            mode = 1;
            sprChange(resource.IngameUI[0]);
            objActiveManage(dirObj);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            mode = 2;
            sprChange(resource.IngameUI[1]);
            objActiveManage(dirObj);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            mode = 3;
            sprChange(resource.IngameUI[1]);
            objActiveManage(ShieldObj);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            mode = 4;
            sprChange(resource.IngameUI[1]);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            AttackSpeed -= 0.1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            AttackSpeed += 0.1f;
        }
    }
    private void Update()
    {
        mouseCheck();
        keyBoardCheck();

        if (mode == 1)  //Player move
        {
            playerMove();
        }
        else if(mode == 3)  //Sheild Summon
        {
            ShieldObj.transform.rotation = Quaternion.AngleAxis(angle - 43, Vector3.forward);   //original 43 -> 90
        }
    }
    private void sprChange(Sprite spr)
    {
        SpriteRenderer sprRender = dirObj.GetComponent<SpriteRenderer>();
        sprRender.sprite = spr;
    }

    private void playerMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-0.05f, 0.00f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(+0.05f, 0.00f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector2(0.00f, -0.05f));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector2(0.00f, +0.05f));
        }
    }

    public float MouseEnergy()
    {
        float enegry;

        enegry = Mathf.Abs(mouseDistance.x) + Mathf.Abs(mouseDistance.y);
        enegry *= 10;

        if (enegry >= 80.0f)
        {
            enegry = 80.0f;
        }
        else if (enegry <= 5.0f)
        {
            enegry = 0.0f;
        }

        return enegry;
    }
    private void gunAttack()
    {
        if (bulletCheck)
        {
            bullet = Instantiate(resource.SkillPre[0], this.transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Bullet bu = bullet.GetComponent<Bullet>();

            bu.Fire(dir, 0);
            bulletCheck = false;

            StartCoroutine(bulletDelay(AttackSpeed * 0.5f));
        }
    }

    IEnumerator bulletDelay(float sec)
    {
        yield return new WaitForSeconds(sec);
        bulletCheck = true;
    }
}
