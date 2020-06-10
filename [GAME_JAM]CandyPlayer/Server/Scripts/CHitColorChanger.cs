using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHitColorChanger : MonoBehaviour
{
    private int time;
    private int interval = 30;
    private int maxInterval = 250;

    private SpriteRenderer spriteRenderer;

    public Color color = Color.red;

    private bool isHit;
    private bool isChange;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        time = 0;
    }

    public void Update()
    {
        if (isHit)
        {
            time++;

            if (time % interval == 0)
            {
                if (!isChange)
                {
                    spriteRenderer.color = color;
                }
                else
                {
                    spriteRenderer.color = Color.white;
                }
                isChange = !isChange;
            }

            if (time >= maxInterval) isHit = false;
        }
    }

    public void Hit()
    {
        time = 0;
        isHit = true;
        isChange = false;
    }
}
