using UnityEngine;

public class PuzzleWorld : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
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
