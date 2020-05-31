using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFunc : MonoBehaviour
{
    public int blockHp;
    SpriteRenderer spr;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        blockHp = 100;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            blockHp -= 10;
            Destroy(collision.gameObject);
            Debug.Log("check");
        }
    }

    private void Update()
    {
        //spr.size = new Vector2(blockHp/ 100, blockHp / 100);
    }
}
