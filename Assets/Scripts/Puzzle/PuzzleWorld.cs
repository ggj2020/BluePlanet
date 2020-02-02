using UnityEngine;

public class PuzzleWorld : MonoBehaviour
{
    void Start()
    {
         var rightCornor = new Vector3(Screen.width, 0, transform.position.z);
        transform.position = Camera.main.ScreenToWorldPoint(rightCornor);
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
