using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCool : MonoBehaviour
{
    public float skillCool;

    void Start()
    {
        skillCool = 0.0f;
        StartCoroutine("coolTime");
    }

    void Update()
    {
        
    }
    IEnumerator CoolTime()
    {
        while(true)
        {
            skillCool += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
