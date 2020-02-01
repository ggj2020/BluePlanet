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
        Vector3 vNewPos = Vector3.Lerp( transform.position, playerUnit.position, 0.1f );
        vNewPos.z = -10;
        transform.position = vNewPos;
    }
}
