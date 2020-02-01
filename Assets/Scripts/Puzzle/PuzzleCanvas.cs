using UnityEngine;

public class PuzzleCanvas : MonoBehaviour
{
    public void OnTouchedJump()
    {
        EventManager.TriggerEvent(new TryJumpEvent());
    }
}
