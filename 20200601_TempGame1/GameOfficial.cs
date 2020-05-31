using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOfficial : MonoBehaviour
{
    public static GameOfficial InstanceOff = null;

    void Awake()
    {
        if(InstanceOff == null)
        {
            InstanceOff = this;
        }
        else if(InstanceOff != null)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = to - from;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    public Vector3 AngleToDirection(GameObject stdObj, float angle)
    {
        Vector3 direction = stdObj.transform.forward;

        var quaternion = Quaternion.Euler(0, angle, 0);
        Vector3 newDirection = quaternion * direction;
        //Vector3 newDirection = new Vector3(quaternion.x, quaternion.y, quaternion.z);

        Debug.Log(newDirection);

        return newDirection;
    }
    public void playSound(AudioClip clip, AudioSource audioPlayer)
    {
        //audioPlayer.Stop();
        audioPlayer.clip = clip;
        //audioPlayer.loop = true;
        //audioPlayer.time = 0f;
        audioPlayer.Play();
    }
    public void falseEnergyBar(bool check, Image im1, Image im2)         //Energy Bar FadeOut
    {
        Color color1 = im1.color;
        Color color2 = im2.color;

        if (check)
        {
            color2.a = 1.0f;

            color1.a = 1.0f;
        }
        else
        {
            color1.a -= 0.1f;
            color2.a -= 0.1f;
        }

        im1.color = color1;
        im2.color = color2;

        StartCoroutine(InterfaceDelay(0.1f));
    }
    public void falseEnergyBar(bool check, Image im1)         //Energy Bar FadeOut
    {
        Color color1 = im1.color;

        if (check)
        {
            color1.a = 1.0f;
        }
        else
        {
            color1.a -= 0.1f;
        }

        im1.color = color1;

        StartCoroutine(InterfaceDelay(0.1f));
    }
    IEnumerator InterfaceDelay(float sec)
    {
        float curSec = 0.0f;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (curSec >= sec)
                break;
        }
    }
}
