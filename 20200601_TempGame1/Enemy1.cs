using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private ResourceManage resource;
    private GameObject player;
    private GameObject bullet;
    private Vector3 dir;
    public int hp;

    private void Start()
    {
        player = GameObject.Find("Player");
        resource = ResourceManage.instanceRes;
        StartCoroutine(bulletDelay(1.0f));

        hp = 100;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hpCheck(hp--);
        }
    }

    private void hpCheck(int Hp)
    {
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void bulletFire()
    {
        /// Debug.Log("총알 발사");
        //transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 8.0f * Time.deltaTime);

        bullet = Instantiate(resource.SkillPre[0], this.transform.position, Quaternion.identity);
        //bullet.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Bullet bu = bullet.GetComponent<Bullet>();

        dir = transform.position - GameObject.Find("Player").transform.position;
        dir = dir.normalized;

        bu.Fire(-dir, 1);
    }
    IEnumerator bulletDelay(float sec)
    {
        float curSec = 0.0f;
        while(true)
        {
            //bulletFire();
            //curSec += 0.1f;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
