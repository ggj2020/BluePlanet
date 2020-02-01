using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GarbageManager : MonoBehaviour
{
    public GameObject[] prefabGarbage;

    private int[] m_nGarbageDistribute;
}

public partial class GarbageManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Assert( prefabGarbage != null );

        Statics.garbageManager = this;

        for ( int nIdx = 0; nIdx < transform.childCount; ++nIdx )
        {
            Transform tChild = transform.GetChild( nIdx );
            m_ltFoothold.Add( tChild );
        }
    }

    private void AllocNextGarbage()
    {

    }
}