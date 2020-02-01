using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleRenderTextureImage : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<RawImage>().SetNativeSize();
    }
}
