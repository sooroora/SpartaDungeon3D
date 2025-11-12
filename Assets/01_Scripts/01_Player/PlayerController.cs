using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    /*
     *  SerializeField 들
     */
    [ SerializeField ] private PlayerMovingStat movingStat;
    [ SerializeField ] private Transform interactionPoint;

    [ SerializeField ] private LayerMask groundLayerMask;
    [ SerializeField ] private LayerMask wallLayerMask;

    [ Header( "Interaction Range" ) ]
    [SerializeField] float interactForwardOffset = 2.0f;
    [SerializeField] float interactheightOffset = 0.0f;
    [SerializeField] float interactMaxDistance = 5.0f;
    [SerializeField] float interactSphereSize = 1.0f;

    [ Header( "Wall Check Distance" ) ]
    [SerializeField] float wallCheckDistance = 0.1f;
    /*
     *
     */
    private Player player;
    public Rigidbody Rigidbody => rb;
    Rigidbody rb;
    private NavMeshAgent agent;


    /*
     *  이동 정보들
     */
    private Vector2 moveInput;
    private Vector3 camForward;
    private Vector3 playerForward;

    bool isDashing;
    bool isHangWall;

    private Vector3 forceMovementPos;


    /*
     *  인터랙션 관련
     */
    RaycastHit[] hits;
    IInteractable nowFocusInteractable;

    private RaycastHit wallHit;

    private void Awake()
    {
        rb = GetComponent< Rigidbody >();
        player = GetComponent< Player >();
        agent = GetComponent< NavMeshAgent >();
        agent.enabled = false;
        hits = new RaycastHit[] {};

        agent.speed = movingStat.Speed;
    }

    private void Update()
    {
        CheckRayHit();
        CheckInteractable();
        CheckWall();
    }

    private void FixedUpdate()
    {
        
        Move();
        
        
        if ( agent.enabled )
        {
            if (  Vector3.Distance( this.transform.position, agent.destination ) < 0.05f )
            {
                agent.enabled = false;
            }
        }
        
        
    }

    void Move()
    {
        //if(wallHit.collider != null) return;
        
        float nowSpeed = movingStat.Speed;

        nowSpeed = nowSpeed * player.Condition.GetSpeedBuffValue();
        
        
        if ( isDashing &&
             player.Condition.Stamina.CurrentValue > 0.0f )
        {
            player.Condition.Dash( Time.deltaTime * 5.0f );
            nowSpeed = nowSpeed * movingStat.DashMultiplier;
        }


        Vector3 prePos = rb.position;//transform.position;

        Vector3 right = Vector3.Cross(Vector3.up, camForward).normalized;
        Vector3 moveDir = (camForward * moveInput.y + right * moveInput.x).normalized;
        
        // float dot = Vector3.Dot(playerForward,moveDir);
        // if ( wallHit.collider != null && dot > 0.85f )
        // {
        //     //Debug.Log(dot);
        //     return;
        // }
        
        // Vector3 nextPos = rb.position + moveDir * (nowSpeed * Time.deltaTime);
        // rb.MovePosition(nextPos);
        
        transform.position += moveDir * (nowSpeed * Time.deltaTime);
        Vector3 nextPos = transform.position;

        // 움직일때만 바뀌게
        if ( moveInput != Vector2.zero )
        {
            playerForward = Vector3.Normalize( nextPos - prePos );
            player.UpdateMovingForward( playerForward );
            agent.enabled = false;
        }
    }

    public void ForceMove( Vector3 movingPos )
    {
        this.transform.position +=
            forceMovementPos;
    }
    
    public void NavMeshMove( Vector3 targetPos )
    {
        agent.enabled = true;
        agent.SetDestination( targetPos );
    }


    public void UpdateForward( Vector3 _forward )
    {
        camForward = _forward;
    }

    public void UpdateMoveInput( Vector2 input )
    {
        moveInput = input;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[ 4 ]
        {
            new Ray( transform.position + ( transform.forward * 0.2f ) + ( transform.up * 0.01f ), Vector3.down ),
            new Ray( transform.position + ( -transform.forward * 0.2f ) + ( transform.up * 0.01f ), Vector3.down ),
            new Ray( transform.position + ( transform.right * 0.2f ) + ( transform.up * 0.01f ), Vector3.down ),
            new Ray( transform.position + ( -transform.right * 0.2f ) + ( transform.up * 0.01f ), Vector3.down )
        };

        for ( int i = 0; i < rays.Length; i++ )
        {
            if ( Physics.Raycast( rays[ i ], 0.1f, groundLayerMask ) )
            {
                return true;
            }
        }

        return false;
    }

    public void CheckRayHit()
    {
        if ( CameraManager.Instance.CameraController.IsThirdPerson )
        {
            Vector3 startPoint = interactionPoint.position + ( playerForward * interactForwardOffset ) + ( Vector3.up * interactheightOffset );

            // interactable 이 layer 가 무조건 Interactable 이지는 않아서 all 로 변경해서 찾기
            hits = Physics.SphereCastAll( startPoint, interactSphereSize, playerForward, interactMaxDistance );
        }
        else
        {
            Ray ray = CameraManager.Instance.Cam.ScreenPointToRay( new Vector3( Screen.width / 2, Screen.height / 2, 0 ) );
            hits = Physics.RaycastAll( ray, interactMaxDistance );
        }

        if ( hits.Length > 0 )
        {
            Array.Sort( hits, ( a, b ) => a.distance.CompareTo( b.distance ) );
        }
    }


    public void CheckInteractable()
    {
        if ( hits.Length > 0 )
        {
            for ( int i = 0; i < hits.Length; i++ )
            {
                if ( hits[ i ].collider.TryGetComponent< IInteractable >( out IInteractable interactable ) )
                {
                    if ( nowFocusInteractable != interactable )
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


    public void CheckWall()
    {
        // if ( hits.Length > 0 )
        // {
        //     ClimbableWall hitWall;
        //     for ( int i = 0; i < hits.Length; i++ )
        //     {
        //         if ( hits[ i ].collider.TryGetComponent( out ClimbableWall wall ) )
        //         {
        //             //Debug.Log(hits[i].distance);
        //
        //             return;
        //         }
        //     }
        // }
        // Physics.SphereCast( interactionPoint.position, wallCheckDistance, playerForward, out wallHit, wallCheckDistance, wallLayerMask );
        // //Physics.Linecast( interactionPoint.position, interactionPoint.position + playerForward * wallCheckDistance, out wallHit, wallLayerMask );
        //
        // if ( wallHit.collider != null )
        // {
        //     Debug.Log( wallHit.distance );
        //     
        // }
    }


    public void ForceJump( float jumpForce )
    {
        rb.AddForce( Vector3.up * jumpForce, ForceMode.Impulse );
    }


    /*
     *  Receive Input
     */
    public void OnJump()
    {
        if ( IsGrounded() )
        {
            rb.AddForce( Vector3.up * movingStat.JumpForce, ForceMode.Impulse );
        }
    }

    public void OnDash( bool _isDashing )
    {
        isDashing = _isDashing;
    }

    public void OnInteraction()
    {
        // 추가 예외처리 생각나는거 있으면 해

        if ( nowFocusInteractable != null )
        {
            nowFocusInteractable.Interaction();
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if ( interactionPoint == null ) return;

        Gizmos.color = Color.red;

        Vector3 startPoint = interactionPoint.position + ( playerForward * interactForwardOffset ) + ( Vector3.up * interactheightOffset );
        Gizmos.DrawWireSphere( startPoint, interactSphereSize );
        Gizmos.DrawWireSphere( startPoint + playerForward * interactMaxDistance, interactSphereSize );
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(
            startPoint,
            startPoint + playerForward * interactMaxDistance );
        
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(interactionPoint.position, interactionPoint.position + playerForward * wallCheckDistance );
    }
#endif
}