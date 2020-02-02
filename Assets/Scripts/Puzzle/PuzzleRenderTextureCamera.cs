using UnityEngine;

public class PuzzleRenderTextureCamera : MonoBehaviour
{
    void Start()
    {
        var targetTexture = GetComponent<Camera>().targetTexture;
        targetTexture.width = Screen.width;
        targetTexture.height = Screen.height;
    }
}
