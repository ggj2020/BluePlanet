using UnityEngine;

public class PuzzleWorld : MonoBehaviour
{
    void Start()
    {
        var rightCornor = new Vector3(Screen.width, 0, transform.position.z);
        var newPos = Camera.main.ScreenToWorldPoint(rightCornor);
        newPos.x -= (GetComponent<SpriteRenderer>().bounds.size.x+2);
        newPos.y -= GetComponent<SpriteRenderer>().bounds.size.y/2;
        transform.position = newPos;
    }

    void Update()
    {
#if UNITY_EDITOR
		if ( Statics.bPause ) return;
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
