using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManage : MonoBehaviour
{
    public static ResourceManage instanceRes = null;

    public Sprite[] IngameUI;

    public AudioClip[] PlayerSE;

    public GameObject[] SkillPre;
    public GameObject[] MapTilePre;
    void Awake()
    {
        if(instanceRes == null)
        {
            instanceRes = this;
        }
        else if(instanceRes != null)
        {
            Destroy(this.gameObject);
        }

        LoadUI();
    }

    public void LoadUI()
    {
        IngameUI = Resources.LoadAll<Sprite>("Sprite/InGameInterface");
        PlayerSE = Resources.LoadAll<AudioClip>("Sound/SE");
        SkillPre = Resources.LoadAll<GameObject>("Prefab/SkillPre");
        MapTilePre = Resources.LoadAll<GameObject>("Prefab/MapTile");
    }
}
