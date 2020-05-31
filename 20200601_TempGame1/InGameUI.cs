using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Image[] Bar;

    public PlayerFunc PlayerFuncS;
    public Text modText, asText, bossHpText;
    public Enemy1 enemy1;

    private bool barCheck;
    private GameOfficial gameofficial;

    public void FindResource()                     //Object All Search Find!!
    {
        Bar = new Image[2];

        Bar[0] = GameObject.Find("EnergyBarOut").GetComponent<Image>();
        Bar[1] = GameObject.Find("EnergyBarIn").GetComponent<Image>();

        modText = GameObject.Find("CurMod").GetComponent<Text>();
        asText = GameObject.Find("CurAS").GetComponent<Text>();
        bossHpText = GameObject.Find("BossHp").GetComponent<Text>();
        PlayerFuncS = GameObject.Find("Player").GetComponent<PlayerFunc>();

    }                

    private void EngeryBarCharge()                  //Energy Bar Power of Color Change
    {
        float energy = PlayerFuncS.MouseEnergy();

        if (energy >= 80.0f) Bar[1].color = Color.red;
        else Bar[1].color = Color.white;

        Bar[1].fillAmount = (PlayerFuncS.MouseEnergy() - 5.0f) / (80.0f - 5.0f);
    }            
    private void Awake()
    {
        gameofficial = GameOfficial.InstanceOff;
        FindResource();

        barCheck = false;
    }
    private void Update()
    {
        string str = PlayerFuncS.mode == 1 ? "이동 모드" : "총 모드";

        switch (PlayerFuncS.mode)
        {
            case 1: str = "이동 모드";break;
            case 2: str = "총 모드";break;
            case 3: str = "쉴드 모드";break;
            case 4: str = "레이저 모드";break;
        }
        
        asText.text = PlayerFuncS.AttackSpeed + "x";
        bossHpText.text = enemy1.hp.ToString();
        modText.text = str;

        if (Input.GetMouseButtonUp(0))
        {
            barCheck = true;
        }
            
        if (Input.GetMouseButtonDown(0))
        {
            gameofficial.falseEnergyBar(true, Bar[0], Bar[1]);
            barCheck = false;
        }

        if (barCheck)
            gameofficial.falseEnergyBar(false, Bar[0], Bar[1]);

        if (!barCheck)
            EngeryBarCharge();
    }


}
