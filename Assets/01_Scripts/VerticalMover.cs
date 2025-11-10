using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    [SerializeField] float moveRange = 0.02f;
    [SerializeField] private float speed = 0.05f; 

    private float originY;
    private void Start()
    {
        originY = this.transform.localPosition.y;
    }

    void Update()
    {
        float newY = originY + Mathf.PingPong(Time.time * speed, moveRange * 2f) - moveRange;
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}
