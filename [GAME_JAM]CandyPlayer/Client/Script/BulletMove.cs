using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private GameObject DIR, PLY;
    private PlayerContr playercon;

    private float tim;
    Vector3 vec3dir;
    void Start()
    {
        
        tim = 0f;
        PLY = GameObject.Find("TestPlayer");
        DIR = GameObject.Find("Dir");

        vec3dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - PLY.transform.position;
        vec3dir.Normalize();

        StartCoroutine("Timers");
        //playercon = GameObject.Find("TestPlayer").GetComponent<PlayerContr>();
    }

    // Update is called once per frame
    void Update()
    { 
        

        transform.position = transform.position + (vec3dir / 2);

        if(tim >= 2.0f)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator Timers()
    {
        while(true)
        {
            tim += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
