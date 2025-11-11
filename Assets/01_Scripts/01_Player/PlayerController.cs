using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    /*
     *  SerializeField 들
     */
    [SerializeField] private PlayerMovingStat movingStat;
    [SerializeField] private Transform interactionPoint;

    [SerializeField] private LayerMask groundLayerMask;

    [Header("Interaction Range")]
    public float interactForwardOffset = 2.0f;

    public float interactheightOffset = 0.0f;
    public float interactMaxDistance = 5.0f;
    public float interactSphereSize = 1.0f;


    /*
     *
     */
    private Player player;

    public Rigidbody Rigidbody => rb;
    Rigidbody rb;


    /*
     *  이동 정보들
     */
    private Vector2 moveInput;
    private Vector3 camForward;
    private Vector3 playerForward;
    bool isDashing;

    private Vector3 forceMovementPos;

    /*
     *  인터랙션 관련
     */
    IInteractable nowFocusInteractable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        CheckInteractable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float nowSpeed = movingStat.Speed;

        if (isDashing &&
            player.Condition.Stamina.CurrentValue > 0.0f)
        {
            player.Condition.Dash(Time.deltaTime * 5.0f);
            nowSpeed = nowSpeed * movingStat.DashMultiplier;
        }

        Vector3 prePos = player.transform.position;

        this.transform.position +=
            camForward * (moveInput.y * nowSpeed * Time.deltaTime);

        Vector3 right = Vector3.Cross(Vector3.up, camForward).normalized;
        this.transform.position +=
            right * (moveInput.x * nowSpeed * Time.deltaTime);

        Vector3 nowPos = player.transform.position;


        // 움직일때만 바뀌게
        if (moveInput != Vector2.zero)
        {
            playerForward = Vector3.Normalize(nowPos - prePos);
            player.UpdateMovingForward(playerForward);
        }
    }
    
    public void ForceMove(Vector3 movingPos)
    {
        this.transform.position +=
            forceMovementPos;
    }


    public void UpdateForward(Vector3 _forward)
    {
        camForward = _forward;
    }

    public void UpdateMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void CheckInteractable()
    {
        RaycastHit[] hits;
        
        if (CameraManager.Instance.CameraController.IsThirdPerson)
        {
            Vector3 startPoint = interactionPoint.position + (playerForward * interactForwardOffset) + (Vector3.up * interactheightOffset);

            // interactable 이 layer 가 무조건 Interactable 이지는 않아서 all 로 변경해서 찾기
             hits = Physics.SphereCastAll(startPoint, interactSphereSize, playerForward, interactMaxDistance);
           
        }
        else
        {
            Ray ray = CameraManager.Instance.Cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            hits = Physics.RaycastAll(ray,interactMaxDistance);
        }
        
        if (hits.Length > 0)
        {
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    if (nowFocusInteractable != interactable)
                    {
                        nowFocusInteractable?.InteractionRangeExit();
                        nowFocusInteractable = interactable;
                        nowFocusInteractable?.InteractionRangeEnter();
                    }
                    return;
                }
            }
        }

        nowFocusInteractable?.InteractionRangeExit();
        nowFocusInteractable = null;

    } 
 

    public void ForceJump(float jumpForce)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

   

    /*
     *  Receive Input
     */
    public void OnJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * movingStat.JumpForce, ForceMode.Impulse);
        }
    }

    public void OnDash(bool _isDashing)
    {
        isDashing = _isDashing;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (interactionPoint == null) return;

        Gizmos.color = Color.red;

        Vector3 startPoint = interactionPoint.position + (playerForward * interactForwardOffset) + (Vector3.up * interactheightOffset);
        Gizmos.DrawWireSphere(startPoint, interactSphereSize);
        Gizmos.DrawWireSphere(startPoint + playerForward * interactMaxDistance, interactSphereSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(
            startPoint,
            startPoint + playerForward * interactMaxDistance);


    }
#endif
}
