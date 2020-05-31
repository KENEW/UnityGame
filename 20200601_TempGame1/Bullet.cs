using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private ResourceManage resource = null;
    private Rigidbody2D rb;

    private Vector3 dirVec3;
    private int state;
    private bool firecheck;

    void Awake()
    {
        resource = ResourceManage.instanceRes;
        rb = GetComponent<Rigidbody2D>();

        firecheck = false;
    }
    public void Fire(Vector3 dir, int state)
    {
        dirVec3 = dir;
        firecheck = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(state == 0)
        {
            if (collision.gameObject.tag == "Boss")
            {
                Destroy(this.gameObject);
            }
        }
        else if(state == 1)
        {
            if (collision.gameObject.tag == "play")
            {
                Destroy(this.gameObject);
            }
        }

    }
    private void Update()
    {
        if (firecheck)
        {
            rb.AddForce(dirVec3 * 15);
        }

        Destroy(this.gameObject, 1f);
    }
}
