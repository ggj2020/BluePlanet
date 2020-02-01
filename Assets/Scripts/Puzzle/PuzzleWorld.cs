using UnityEngine;

public class PuzzleWorld : MonoBehaviour
{
    [SerializeField] private Camera renderTextureCamera = null;
    void Start()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        var rightEdgePosition = new Vector3(1.0f, 0.5f, transform.localPosition.z);
        //Debug.Log(renderer.bounds);
        var newPos = renderTextureCamera.ViewportToWorldPoint(rightEdgePosition);
        newPos.x -= renderer.bounds.extents.x;
        transform.position = newPos;
    }

    void Update()
    {
#if UNITY_EDITOR
		if ( !Statics.bPause ) return;
        if( Input.GetKeyDown(KeyCode.Return) )
        {
            EventManager.TriggerEvent(new ShakePuzzleEvent());
        }
        if( Input.GetKeyDown(KeyCode.Space) )
        {
            EventManager.TriggerEvent(new GeneratePuzzleEvent());
        }
#endif
    }
}
