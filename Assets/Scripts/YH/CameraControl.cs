using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraControl : MonoBehaviour
{
    public PlayerUnit playerUnit;
}

public partial class CameraControl : MonoBehaviour
{
    public float distanceToCharacter = -10.0f;
    public Vector2 offset = new Vector2(5.0f, 0.0f);
    
    [Range(0.0f, 1.0f)]
    public float u;

    private Vector3 priviousPosition;

    private void FixedUpdate()
    {
        var targetPosition = playerUnit.position;
        targetPosition.z = distanceToCharacter;
        targetPosition.x += offset.x;
        targetPosition.y += offset.y;
        transform.position = Vector3.Lerp(priviousPosition, targetPosition, u);




        priviousPosition = transform.position;
    }
}
