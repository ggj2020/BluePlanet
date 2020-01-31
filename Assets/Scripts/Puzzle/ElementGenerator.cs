using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGenerator : MonoBehaviour
{

    private string[] elements = new string[] {
        "Puzzle/Prefabs/Pentagon",
        "Puzzle/Prefabs/RoundedRectangle",
        "Puzzle/Prefabs/Triangle"
    };

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space) )
        {
            this.Generate();
        }
    }

    void Generate()
    {
        GameObject prefab = Resources.Load<GameObject>(elements.Pick());
        Instantiate<GameObject>(prefab, transform);
    }
}
