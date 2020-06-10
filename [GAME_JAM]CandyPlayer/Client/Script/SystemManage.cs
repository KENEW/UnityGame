using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManage : MonoBehaviour
{
    public PlayerContr player;
    private int SecondTime;
    private int Fades;
    public Text Count;
    public Text TIME;
    public Text CURITEM;
    public Text FEVER;
    public Text WIN;
    public Text DEFEAT;



    void Start()
    {
        FEVER.enabled = false;
        WIN.enabled = false;
        SystemInit();
        Fades = 0;
        StartCoroutine("Timer");
        StartCoroutine("Fade");

    }
    void SystemInit()
    {
        SecondTime = 300;
    }
    // Update is called once per frame
    void Update()
    {
        if(SecondTime <= 60)
        {
            FEVER.enabled = true;
            TIME.fontSize = 80 + Fades;
            TIME.color = Color.red;
        }
        if(SecondTime == 0)
        {
            StopCoroutine("Timer");
            WIN.enabled = true;

        }
        TIME.text = (SecondTime/60).ToString() + " : " + (SecondTime % 60).ToString();
        Count.text = (player.SecondForm).ToString();
        CURITEM.text = "현재 아이템 : " + player.curItem;
    }
    IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            SecondTime -= 1;
        }
       
    }
    IEnumerator Fade()
    {
        int cendo = 1;
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            Fades += 1 * cendo;
            if (Fades == 10)
            {
                cendo = -1;
            }
            else if(Fades == 0)
            {
                cendo = 1;
            }
        }
    }
}
