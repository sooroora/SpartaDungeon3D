using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private Transform platform;
    
    [Header("Moving Option")]
    [SerializeField] bool isHorizontal = true;
    [SerializeField] float moveRange = 1.0f;
    [SerializeField] private float speed = 0.2f; 
    
    private float originPos;
    private Vector3 nowMovingPos;
    
    private void Start()
    {
        if (isHorizontal)
            originPos = platform.localPosition.x;
        else
            originPos = platform.localPosition.y;

    }

    void Update()
    {
        //sin, pingpong 타입 설정 할 수도 있게 
        float offset = Mathf.Sin(Time.time * speed) * moveRange;
       
        
        float newPos = originPos + offset;
        
        if(isHorizontal)
            platform.localPosition = new Vector3(newPos, platform.localPosition.y, platform.localPosition.z);
        else
        {
            platform.localPosition = new Vector3(platform.localPosition.x, newPos, platform.localPosition.z);
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 worldPos = transform.position;

        if (isHorizontal)
        {
            float minX = worldPos.x - moveRange;
            float maxX = worldPos.x + moveRange;

            Gizmos.DrawLine(
                new Vector3(minX, worldPos.y, worldPos.z),
                new Vector3(maxX, worldPos.y, worldPos.z)
            );
        }
        else
        {
            float minY = worldPos.y - moveRange;
            float maxY = worldPos.y + moveRange;

            Gizmos.DrawLine(
                new Vector3(worldPos.x, minY, worldPos.z),
                new Vector3(worldPos.x, maxY, worldPos.z)
            );
        }
    }
}
