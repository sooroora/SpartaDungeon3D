using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    private Vector3 nowMovingPos;
    private Vector3 prevPos;
    
    private void Start()
    {
        prevPos = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 current = transform.position;
        nowMovingPos = current - prevPos;
        prevPos = current;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            
            player.transform.SetParent(followTransform, true);
            
            // parent 안 붙이고 움직이고 싶은데 Player의 FixedUpdate에서 움직이게 해도 플랫폼이 덜그럭 거리는 문제가 있음
            // 다른 거 먼저 처리하고 시간남으면 생각하기
            //player.Controller.ForceMove(nowMovingPos);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.transform.SetParent(null,true);
            player.transform.localScale = Vector3.one;
            //player.Controller.ForceMove(nowMovingPos);
        }
    }


}
