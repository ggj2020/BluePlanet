using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CameraLeftEdgeAligner : MonoBehaviour
{
    void Start()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        Vector3 leftEdgePosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.localPosition.z));
        leftEdgePosition.x += renderer.bounds.size.x/2;
        leftEdgePosition.y += renderer.bounds.size.y/2;
        transform.localPosition = leftEdgePosition;
    }
}
