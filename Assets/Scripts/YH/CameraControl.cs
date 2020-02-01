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
        var targetPosition = playerUnit.position;
        // targetPosition.x += 10f;

        // Vector3 vNewPos = Vector3.Lerp( transform.position, playerUnit.position, 0.`1f );
        // vNewPos.z = -10;
        targetPosition.z = -10f;
        targetPosition.x += 5f;
        transform.position = targetPosition;
    }
}
