using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GarbageManager : MonoBehaviour
{
    public GameObject[] prefabGarbage;

    private int[] m_nGarbageDistribute;
    private Queue<Garbage> m_qGarbage;
}

public partial class GarbageManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Assert( prefabGarbage != null );

        Statics.garbageManager = this;

        m_nGarbageDistribute = new int[ prefabGarbage.Length ];
        m_qGarbage = new Queue<Garbage>();
    }

    private void FixedUpdate()
    {
        while ( m_qGarbage.Count < 10 )
        {

        }
    }

    private Garbage GetNextGarbage()
    {
        Garbage g;

        int nCountTotal = 0;
        foreach( int nCount in m_nGarbageDistribute )
        {
            nCountTotal += nCount;
        }

        int nRand = Random.Range( 0, nCountTotal );
        int nCountSum = 
        if(nRand < )
    }
}