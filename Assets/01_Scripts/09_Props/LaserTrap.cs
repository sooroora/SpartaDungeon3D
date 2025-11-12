using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{

    [SerializeField] private Transform laserStartPivot;
    [SerializeField] private Transform laserEndPivot;
    
    [SerializeField] private Transform laserScaler;


    [ SerializeField ] private int damage = 10;
    private void Start()
    {
        SetLaserLength();
    }

    private void Update()
    {
        Ray ray = new Ray(laserStartPivot.position, Vector3.Normalize(laserEndPivot.position -  laserStartPivot.position));
        RaycastHit[] hits  = Physics.RaycastAll(ray, Vector3.Distance( laserStartPivot.position, laserEndPivot.position ) );
        if ( hits.Length>0)
        {
            foreach ( RaycastHit hit in hits )
            {
                if ( hit.collider.TryGetComponent( out Player player ) )
                {
                    player.Condition.TakeDamage(damage);
                }
            }
            // if ( hitInfo.collider.TryGetComponent( out Player player ) )
            // {
            //     // 플레이어한테 데미지
            //     // Debug.Log("어쩌구");
            // }
        }

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
