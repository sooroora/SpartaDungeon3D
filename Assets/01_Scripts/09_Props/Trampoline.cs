using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 20f; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Controller.ForceJump(jumpForce);
        }
    }

}
