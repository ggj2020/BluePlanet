

using System.Collections.Generic;
using UnityEngine;

static public class Util
{
    static public List<Foothold> FindFootholdListUnderneath( Vector3 vPos )
    {
        List<Foothold> lfh = new List<Foothold>();
        RaycastHit rh;

        if ( Physics.Raycast( vPos, Vector3.down, out rh, 10.0f ) )
        {
            if ( rh.collider.CompareTag( Constant.TAG_FOOTHOLD ) )
            {
                Foothold fh = rh.collider.gameObject.GetComponent<Foothold>();
                if ( fh ) lfh.Add( fh );
            }
        }

        return lfh;
    }
}