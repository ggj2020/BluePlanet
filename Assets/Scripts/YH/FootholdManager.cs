using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class FootholdManager : MonoBehaviour
{
    private List<Transform> m_ltFoothold;
}

public partial class FootholdManager : MonoBehaviour
{
    private void Awake()
    {
        Statics.footholdManager = this;

        m_ltFoothold = new List<Transform>();

        Transform tBase = transform.Find( "Base" );
        for ( int nIdx = 0; nIdx < tBase.childCount; ++nIdx )
        {
            Transform tChild = tBase.GetChild( nIdx );
            m_ltFoothold.Add( tChild );
        }
    }

    public void SwipeFoothold( bool bSwipeLeft )
    {
        float fFootholdTranslate = ( m_ltFoothold.Count ) * Constant.FOOTHOLD_WIDTH;
        if ( bSwipeLeft ) fFootholdTranslate *= -1;

        Transform tFh;
        if ( bSwipeLeft )
        {
            tFh = m_ltFoothold[ m_ltFoothold.Count - 1 ];
            m_ltFoothold.Remove( tFh );
            m_ltFoothold.Insert( 0, tFh );
        }
        else
        {
            tFh = m_ltFoothold[ 0 ];
            m_ltFoothold.Remove( tFh );
            m_ltFoothold.Add( tFh );
        }

        tFh.position += new Vector3( fFootholdTranslate, 0, 0 );
    }

    public Foothold GetFootholdUnderneath( Vector3 vPos )
    {
        Foothold fh = null;

        RaycastHit rh;
        if ( Physics.Raycast( vPos, Vector3.down, out rh ) )
        {
            if ( rh.collider.CompareTag( Constant.TAG_FOOTHOLD ) )
                fh = rh.collider.gameObject.GetComponent<Foothold>();
        }

        return fh;
    }
}