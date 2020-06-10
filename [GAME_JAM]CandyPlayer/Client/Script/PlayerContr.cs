using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContr : MonoBehaviour
{
    private const float MoveX = 0.1f;
    private const float MoveY = 0.1f;
    public int curItem;
    public bool wallCheck;
    public bool playerDeath;
    public float posX, posY;
    public int SecondForm;
    public Sprite[] CandySprite = new Sprite[6];
    public SpriteRenderer CandyRender;

    public int PlayerHP;

    //0 = 없음, 1 = 슈팅, 2 = 부메랑
    public int CurItem;

    public GameObject Dir;
    private GameObject Bullet;

    public Quaternion mQut;
    private Vector3 resultPoint;

    void Start()
    {
        curItem = 0;
        SecondForm = 3000;
        Bullet = Resources.Load("Prefab/Bullet") as GameObject;

        CandyRender = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine("ReForm");
        posX = this.transform.position.x;
        posY = this.transform.position.y;
        PlayerHP = 4;
        wallCheck = false;
        playerDeath = false;
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
        if(coll.gameObject.tag == "item1")
        {
            StartCoroutine("ReForm");
            CandyRender.sprite = CandySprite[0];
            curItem = 1;
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "item2")
        {
            StartCoroutine("ReForm");

            CandyRender.sprite = CandySprite[1];
            curItem = 2;
            Destroy(coll.gameObject);

        }
        if (coll.gameObject.tag == "item3")
        {
            StartCoroutine("ReForm");

            CandyRender.sprite = CandySprite[2];
            curItem = 3;
            Destroy(coll.gameObject);

        }
        if (coll.gameObject.tag == "item4")
        {
            StartCoroutine("ReForm");

            CandyRender.sprite = CandySprite[3];
            curItem = 4;
            Destroy(coll.gameObject);

        }
        if (coll.gameObject.tag == "item5")
        {
            StartCoroutine("ReForm");

            CandyRender.sprite = CandySprite[4];
            curItem = 5;
            Destroy(coll.gameObject);

        }
        if (coll.gameObject.tag == "item6")
        {
            StartCoroutine("ReForm");

            CandyRender.sprite = CandySprite[5];
            curItem = 6;
            Destroy(coll.gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "wall")
            wallCheck = false;
    }

    void Update()
    {
        resultPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        Vector2 offset = new Vector2(resultPoint.x, resultPoint.y);
        float mangle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        mQut = Quaternion.Euler(new Vector3(0, 0, mangle));

        Dir.transform.rotation = Quaternion.Slerp(Dir.transform.rotation, mQut, 0.2f);

        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate(Bullet, this.transform.position, Quaternion.identity);
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(!wallCheck) posX -= MoveX;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!wallCheck) posX += MoveX;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!wallCheck) posY += MoveY;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!wallCheck) posY -= MoveY;
        }
        this.transform.position = new Vector2(posX, posY);
    }
    IEnumerator ReForm()
    {
        SecondForm = 3;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            SecondForm -= 1;

            if(SecondForm == 0)
            {
                CandyRender.sprite = CandySprite[6];
                StopCoroutine("ReForm");
            }
        }
    }
}
