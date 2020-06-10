using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayer : MonoBehaviour
{
    private const float MoveX = 0.135f;
    private const float MoveY = 0.135f;
    private Vector2 worldMousePos;
    public GameObject AttackLine;
    public int BossHP;
    private float Dash;
    public float xPos;
    public float yPos;
    private bool DelayCheck;
    private float Delay1;
    public Quaternion mQut;
    private Vector3 resultPoint;
    private Vector3 SkillPos;

    void Start()
    {
        DelayCheck = true;
        BossHP = 10;
        xPos = 0;
        yPos = 0;
        Delay1 = 0.0f;
        Dash = 100f;
        AttackLine.GetComponent<BoxCollider2D>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        resultPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        Vector2 offset = new Vector2(resultPoint.x, resultPoint.y);
        float mangle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        mQut = Quaternion.Euler(new Vector3(0, 0, mangle));

        AttackLine.transform.rotation = Quaternion.Slerp(AttackLine.transform.rotation, mQut, 0.2f);
        if (Input.GetMouseButtonDown(0))
        {
            if(DelayCheck)
            {
                BossHP -= 1;
                AttackLine.GetComponent<BoxCollider2D>().enabled = true;
                StartCoroutine("StDelay");
                DelayCheck = false;
            }
       
        }
        else
        {
            AttackLine.GetComponent<BoxCollider2D>().enabled = false;
        }


        if (Input.GetKey(KeyCode.W))
        {
            yPos += MoveY * Dash;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yPos -= MoveY * Dash;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xPos -= MoveX * Dash;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xPos += MoveX * Dash;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //StartCoroutine("StDelay");
           // if (Delay1 >= 2.0f)
            //{
            SkillPos = resultPoint;
            SkillPos.Normalize();
            Dash = 1f;
            //    StopCoroutine("StDelay");
          //   }

        }
        else
            Dash = 1f;
        transform.position = transform.position + (SkillPos);
        this.transform.position = new Vector2(xPos, yPos);
    }
    IEnumerable StDelay()
    {
        int DelaySt = 3;
        while(true)
        { 
            yield return new WaitForSeconds(1f);
            DelaySt -= 1;
            if(DelaySt == 0)
            {
                DelayCheck = true;
            }
        }
    }
}
