using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerUnit : RunObject
{
    public enum INPUT {
        MOVE_LEFT,
        MOVE_RIGHT,
        JUMP_UP
    }
}

public partial class PlayerUnit : RunObject
{
    private Vector2Int m_vVel;

    private List<Foothold> m_lFhCurrent;

    private bool m_bLMoveInput;
    private bool m_bRMoveInput;
    private bool m_bLeft;
    private bool m_bAttatchedOnFoothold;

    private int m_nJumpCount;
    private int m_nFootholdIdx;
}

public partial class PlayerUnit : RunObject
{
    private void Awake()
    {
        m_vVel = Vector2Int.zero;

        m_lFhCurrent = new List<Foothold>();

        m_bLMoveInput = false;
        m_bRMoveInput = false;
        m_bLeft = false;
        m_bAttatchedOnFoothold = false;

        m_nJumpCount = 0;
        m_nFootholdIdx = ( int )position.x;
    }

    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.LeftArrow ) )
            OnInputDown( INPUT.MOVE_LEFT );
        if ( Input.GetKeyDown( KeyCode.RightArrow ) )
            OnInputDown( INPUT.MOVE_RIGHT );

        if ( Input.GetKeyDown( KeyCode.Space ) )
            OnInputDown( INPUT.JUMP_UP );

        if ( Input.GetKeyUp( KeyCode.LeftArrow ) )
            OnInputUp( INPUT.MOVE_LEFT );
        if ( Input.GetKeyUp( KeyCode.RightArrow ) )
            OnInputUp( INPUT.MOVE_RIGHT );
    }
    private void FixedUpdate()
    {
        FUpdateMovement();
        FUpdateFoothold();
    }
    private void FUpdateMovement()
    {
        int nAccX = m_vVel.x == 0 ? 0 : -GetFriction();
        if ( m_bLMoveInput ) nAccX -= Constant.ACCELERATION_DEFAULT;
        if ( m_bRMoveInput ) nAccX += Constant.ACCELERATION_DEFAULT;

        if ( ( m_vVel.x + nAccX > 0 && m_vVel.x < 0 ) ||
            ( m_vVel.x + nAccX < 0 && m_vVel.x > 0 ) )
        {
            m_vVel.x = 0;
            return;
        }

        m_vVel.x += nAccX;
        m_vVel.x = Mathf.Clamp( m_vVel.x, -Constant.VELOCITY_MAX_DEFAULT, Constant.VELOCITY_MAX_DEFAULT );

        if ( IsOnFoothold() && m_bAttatchedOnFoothold )
        {
            m_vVel.y = 0;
        }
        else
        {
            m_vVel.y -= Constant.GRAVITY_DEFAULT;
            m_vVel.y = Mathf.Clamp( m_vVel.y, -Constant.JUMPSPEED_MAX_DEFAULT, Constant.JUMPSPEED_MAX_DEFAULT );
        }

        MovePosition( m_vVel.x * 0.001f, m_vVel.y * 0.001f );
    }
    private void FUpdateFoothold()
    {
        int nNewFootholdIdx = ( int )position.x;
        int nDiff = nNewFootholdIdx - m_nFootholdIdx;
        if ( nDiff > 0 ) Statics.footholdManager.SwipeFoothold( false );
        if ( nDiff < 0 ) Statics.footholdManager.SwipeFoothold( true );

        m_nFootholdIdx = nNewFootholdIdx;
    }

    public void OnInputDown( INPUT eInput )
    {
        switch ( eInput )
        {
            case INPUT.MOVE_LEFT:
            {
                m_bLMoveInput = true;
                m_bLeft = true;
                break;
            }
            case INPUT.MOVE_RIGHT:
            {
                m_bRMoveInput = true;
                m_bLeft = false;
                break;
            }
            case INPUT.JUMP_UP:
            {
                Jump();
                break;
            }
        }
    }
    public void OnInputUp( INPUT eInput )
    {
        switch ( eInput )
        {
            case INPUT.MOVE_LEFT:
            {
                m_bLMoveInput = false;
                break;
            }
            case INPUT.MOVE_RIGHT:
            {
                m_bRMoveInput = false;
                break;
            }
        }
    }

    private void OnTriggerEnter( Collider c )
    {
        if ( c.CompareTag( Constant.TAG_FOOTHOLD ) )
        {
            Foothold fh = c.gameObject.GetComponent<Foothold>();
            if ( fh )
            {
                m_lFhCurrent.Add( fh );
                m_bAttatchedOnFoothold = true;
                m_nJumpCount = 0;
            }
        }

        if ( c.CompareTag( Constant.TAG_GARBAGE ) )
        {
            Garbage g = c.gameObject.GetComponent<Garbage>();
            if ( g )
            {
                Debug.Log( $"{g.objectIndex} get!" );
                g.Deactivate();
                Destroy( c.gameObject );
            }
        }
    }
    private void OnTriggerExit( Collider c )
    {
        if ( c.CompareTag( Constant.TAG_FOOTHOLD ) )
        {
            Foothold fh = c.gameObject.GetComponent<Foothold>();
            if ( fh ) m_lFhCurrent.Remove( fh );
        }
    }

    private void MovePosition( float nX, float nY )
    {
        Vector3 vPos = transform.position;
        vPos.x += nX;
        vPos.y += nY;
        transform.position = vPos;
    }

    private void Jump()
    {
        if ( m_nJumpCount >= Constant.JUMPCOUNT_LIMIT ) 
            return;

        m_vVel.y = Constant.JUMPSPEED_DEFAULT;
        m_bAttatchedOnFoothold = false;
        ++m_nJumpCount;
    }

    private int GetFriction()
    {
        int nFriction = IsOnFoothold() ? m_lFhCurrent[ 0 ].nFriction : Constant.FRICTION_DEFAULT;
        return m_bLeft ? -nFriction : nFriction;
    }

    private bool IsOnFoothold() { return m_lFhCurrent.Count > 0; }
}
