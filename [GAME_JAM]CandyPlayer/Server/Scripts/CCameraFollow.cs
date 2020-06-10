using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraFollow : MonoBehaviour
{
    public static CCameraFollow instance;
    [HideInInspector]
    public Transform followTarget;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (followTarget == null) return;

        this.transform.position =
            new Vector3(
                followTarget.position.x,
                followTarget.position.y,
                -10.0f);
    }
}
