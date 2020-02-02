using UnityEngine;

public class PuzzleCanvas : MonoBehaviour
{
    public void OnTouchedJump()
    {
        EventManager.TriggerEvent(new TryJumpEvent());
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            EventManager.TriggerEvent(new TryJumpEvent());
        }
    }
}
