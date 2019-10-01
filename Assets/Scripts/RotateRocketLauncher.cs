using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRocketLauncher : MonoBehaviour
{
    public Transform target;


    void Update()
    {
        if(target != null)
        {
            LookAt();
        }
        else
        {
            try
            {
                target = GameObject.FindGameObjectWithTag("CrossHair").transform;
            }
            catch
            {
                Debug.Log("Target Lost");
            }
        }
    }

    void LookAt()
    {
        var dir = target.position - transform.position;
        var angle = 360 - Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
