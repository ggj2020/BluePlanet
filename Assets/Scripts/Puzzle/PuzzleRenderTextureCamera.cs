using UnityEngine;
using UnityEngine.UI;

public class PuzzleRenderTextureCamera : MonoBehaviour
{
    [SerializeField] private RawImage renderTarget = null;
    void Start()
    {
        var targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponent<Camera>().targetTexture = targetTexture;
        renderTarget.texture = targetTexture;
        renderTarget.SetNativeSize();
        renderTarget.gameObject.SetActive(true);
    }
}
