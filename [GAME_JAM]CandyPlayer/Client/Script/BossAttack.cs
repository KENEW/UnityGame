using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private CircleCollider2D AttackLine;

    private void Awake()
    {
        AttackLine = this.gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("BossAttack");
        if (coll.gameObject.CompareTag("Player"))
        {
            CEntity entity = coll.gameObject.GetComponent<CEntity>();
            entity.Hit();
        }

        AttackLine.enabled = false;
    }
}
