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
            //player.transform.localScale = new Vector3(1f/this.transform.localScale.x, 1f/this.transform.localScale.y, 1f/this.transform.localScale.z);
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
