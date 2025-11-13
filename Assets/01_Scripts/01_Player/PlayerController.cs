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
    [ SerializeField ] float interactForwardOffset = 2.0f;

    [ SerializeField ] float interactheightOffset = 0.0f;
    [ SerializeField ] float interactMaxDistance = 5.0f;
    [ SerializeField ] float interactSphereSize = 1.0f;

    [ Header( "Wall Check Distance" ) ]
    [ SerializeField ] float wallCheckDistance = 0.1f;
    [ SerializeField ] float wallCheckBottomHeightOffset = 1.6f;

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
    bool isJumping = false;

    private Vector3 forceMovementPos;


    /*
     *  인터랙션 관련
     */
    RaycastHit[] hits;
    IInteractable nowFocusInteractable;

    private RaycastHit bottomWallHit;
    private RaycastHit topWallHit;
    private float wallRayHitDistance;

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
            if ( Vector3.Distance( this.transform.position, agent.destination ) < 0.05f )
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

        Vector3 prePos = transform.position;
        Vector3 nextPos = prePos;

        // 벽타기~ 박은 벽에 따라서 움직이게
        if ( isHangWall )
        {
            Vector3 wallDirForward = Vector3.Cross( bottomWallHit.normal, Vector3.up ).normalized;
            Vector3 dirUp = Vector3.Cross( wallDirForward, bottomWallHit.normal ).normalized;

            // moveInput.y를 위/아래(벽을 타고 올라가거나 내려감), moveInput.x를 좌우(벽을 따라)
            Vector3 moveDir = ( dirUp * moveInput.y + wallDirForward * moveInput.x ).normalized;


            transform.position += moveDir * ( nowSpeed * Time.deltaTime );
            nextPos = transform.position;


            // 카메라 y 만 (up 빼
            Vector3 camForwardFlat = Vector3.ProjectOnPlane( camForward, Vector3.up ).normalized;
            Vector3 camRight = Vector3.Cross( Vector3.up, camForwardFlat ).normalized;
            Vector3 rotateDir = ( camForwardFlat * moveInput.y + camRight * moveInput.x );
            if ( rotateDir != Vector3.zero )
            {
                playerForward = rotateDir.normalized;
                player.UpdateMovingForward( playerForward );
                agent.enabled = false;
            }
        }
        else
        {
            Vector3 right = Vector3.Cross( Vector3.up, camForward ).normalized;
            Vector3 moveDir = ( camForward * moveInput.y + right * moveInput.x ).normalized;

            transform.position += moveDir * ( nowSpeed * Time.deltaTime );
            nextPos = transform.position;

            // 움직일때만 바뀌게
            if ( moveInput != Vector2.zero )
            {
                playerForward = Vector3.Normalize( nextPos - prePos );
                player.UpdateMovingForward( playerForward );
                agent.enabled = false;
            }
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
                if ( isJumping ) isJumping = false;
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
        Vector3 topStart = interactionPoint.position + (playerForward * -0.5f);
        Vector3 topEnd = topStart + (playerForward * wallCheckDistance);
        Vector3 bottomStart = topStart +(Vector3.up * wallCheckBottomHeightOffset );
        Vector3 bottomEnd = bottomStart + (playerForward * wallCheckDistance);
        
        Physics.Linecast( topStart, topEnd, out topWallHit, wallLayerMask );
        Physics.Linecast( bottomStart ,bottomEnd, out bottomWallHit, wallLayerMask );
        //
        // RaycastHit topBottomhit;
        // Physics.Linecast(topEnd, bottomEnd, out topBottomhit, wallLayerMask );
        
        if ( bottomWallHit.collider != null || topWallHit.collider != null )
        {
            rb.useGravity = false;
            isHangWall = true;

            if ( isJumping )
                rb.velocity = Vector3.zero;
        }
        else
        {
            rb.useGravity = true;
            isHangWall = false;
        }
        
    }


    public void ForceJump( float jumpForce )
    {
        isJumping = true;
        rb.AddForce( Vector3.up * jumpForce, ForceMode.Impulse );
    }

    public void ForceJump( float jumpForce, Vector3 dir )
    {
        isJumping = true;
        rb.AddForce( dir* jumpForce, ForceMode.Impulse );
    }


    /*
     *  Receive Input
     */
    public void OnJump()
    {
        if ( IsGrounded() )
        {
            rb.AddForce( Vector3.up * movingStat.JumpForce, ForceMode.Impulse );
            isJumping = true;
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
        
        
        Vector3 topStart = interactionPoint.position + (playerForward * -0.5f);
        Vector3 topEnd = topStart + (playerForward * wallCheckDistance);
        Vector3 bottomStart = topStart +(Vector3.up * wallCheckBottomHeightOffset );
        Vector3 bottomEnd = bottomStart + (playerForward * wallCheckDistance);
        
        Gizmos.DrawLine( topStart, topEnd);
        Gizmos.DrawLine( bottomStart, bottomEnd);
    }
#endif
}