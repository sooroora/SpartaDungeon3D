using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    [SerializeField] private Transform modelRoot;

    public void Rotate(Vector3 forward)
    {
        modelRoot.Rotate(forward);
    }
}
