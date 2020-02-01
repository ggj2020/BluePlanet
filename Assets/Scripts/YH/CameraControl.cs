using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraControl : MonoBehaviour
{
    public PlayerUnit playerUnit;
}

public partial class CameraControl : MonoBehaviour
{
    private void Update()
    {
        transform.position = playerUnit.position + new Vector3( 0, 0, -10 );
    }
}
