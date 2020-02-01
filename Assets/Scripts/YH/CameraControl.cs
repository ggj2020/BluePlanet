using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraControl : MonoBehaviour
{
    public PlayerUnit playerUnit;
}

public partial class CameraControl : MonoBehaviour
{
    private void FixedUpdate()
    {
		if ( !Statics.bPause ) return;

        Vector3 vNewPos = Vector3.Lerp( transform.position, playerUnit.position, 0.1f );
        vNewPos.z = -10;
        transform.position = vNewPos;    }
}
