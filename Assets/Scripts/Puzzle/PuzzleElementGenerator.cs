using UnityEngine;
using System.Linq;

public class PuzzleElementGenerator : MonoBehaviour
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
        if( Input.GetKeyDown(KeyCode.Return) )
        {
            this.Shake();
        }
    }

    private void Generate()
    {
        GameObject prefab = Resources.Load<GameObject>(elements.Pick());
        Instantiate<GameObject>(prefab, transform);
    }

    private void Shake()
    {
        GetComponentsInChildren<Rigidbody2D>().ToList().ForEach((rigidBody2d) => {
            rigidBody2d.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(5f, 10f)), ForceMode2D.Impulse);
        });
    }
}
