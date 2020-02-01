using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GarbageManager : MonoBehaviour
{
    public GameObject[] prefabGarbage;

    public float height_Lv1;
    public float height_Lv2;
    public float offsetX;
    public int garbageGap;

    private Queue<Garbage> m_qGarbage;
    private int m_nDistributeConst;
}

public partial class GarbageManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Assert( prefabGarbage != null );

        Statics.garbageManager = this;

        m_qGarbage = new Queue<Garbage>();
        m_nDistributeConst = 10000 / prefabGarbage.Length;
    }

    private void Start()
    {
        GenerateGarbage( Statics.playerUnit.position.x );
    }

    public void GenerateGarbage( float fX )
    {
        if ( ( int )fX % garbageGap > 0 ) return;
        int nRand = Random.Range( 0, 10000 );
        int nGarbageIdx = nRand / m_nDistributeConst;

        GameObject oGarbage = Instantiate( prefabGarbage[ nGarbageIdx ], transform );

        Vector3 vGarbage = oGarbage.transform.position;
        vGarbage.x = fX + offsetX;
        vGarbage.y = nRand % 2 == 0 ? height_Lv1 : height_Lv2;
        oGarbage.transform.position = vGarbage;

        m_qGarbage.Enqueue( oGarbage.GetComponent<Garbage>() );
    }
}