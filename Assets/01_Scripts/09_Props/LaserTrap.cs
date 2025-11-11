using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{

    [SerializeField] private Transform laserStartPivot;
    [SerializeField] private Transform laserEndPivot;
    [SerializeField] private Transform laserScaler;

    private void Start()
    {
        SetLaserLength();
    }


    [ContextMenu("Connect Laser Pivot")]
    void SetLaserLength()
    {
        float dis = Vector3.Distance(laserStartPivot.position, laserEndPivot.position);

        laserScaler.position = laserStartPivot.position;
        laserScaler.localScale = new Vector3(laserScaler.localScale.x,laserScaler.localScale.y, dis);
        
        Vector3 dir = laserEndPivot.position - laserStartPivot.position;
        if (dir != Vector3.zero)
        {
            laserScaler.rotation = Quaternion.LookRotation(-dir);
        }
    }
}
