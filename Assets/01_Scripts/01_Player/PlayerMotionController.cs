using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    [SerializeField] private Transform modelRoot;

    public void Rotate(Vector3 forward)
    {
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
