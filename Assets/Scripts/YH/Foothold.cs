using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Foothold : RunObject
{
    public int nFriction { get; private set; }
    public Vector3 top { get { return position + Constant.FOOTHOLD_RADIUS * Vector3.up; } }
}

public partial class Foothold : RunObject
{
    private void Awake()
    {
        nFriction = Constant.FRICTION_DEFAULT;
    }

    public float GetYByX( float nX )
    {
        return position.y;
    }
}
