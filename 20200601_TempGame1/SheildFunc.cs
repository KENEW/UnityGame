using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildFunc : MonoBehaviour
{
    private GameOfficial gameOfficial;

    private AudioSource audioComp;
    public AudioClip shieldSE;

    private void Start()
    {
        audioComp = GetComponent<AudioSource>();
        gameOfficial = GameOfficial.InstanceOff;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            gameOfficial.playSound(shieldSE, audioComp);
            Destroy(collision.gameObject);
        }
    }

}
