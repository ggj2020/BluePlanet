using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CameraRightEdgeAligner : MonoBehaviour
{
    void Start()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        Vector3 leftEdgePosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 5f));
        leftEdgePosition.y = 0;
        leftEdgePosition.x += renderer.bounds.size.x/2;
        leftEdgePosition.x *= -1;
        transform.localPosition = leftEdgePosition;
    }
}
