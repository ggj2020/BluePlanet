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
        targetPosition.z = -10f;
        targetPosition.x += 5f;
        transform.position = targetPosition;
    }
}
