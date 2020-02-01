using UnityEngine;
using System.Linq;

public class PuzzleElementGenerator : MonoBehaviour
{

    private string[] elements = new string[] {
        "Puzzle/Prefabs/Pentagon",
        "Puzzle/Prefabs/RoundedRectangle",
        "Puzzle/Prefabs/Triangle"
    };

    void OnEnable()
    {
        EventManager.StartListening(typeof(ShakePuzzleEvent), OnShake);
        EventManager.StartListening(typeof(GeneratePuzzleEvent), OnGenerate);
        EventManager.StartListening(typeof(GarbageAcquireEvent), OnGenerate);
    }

    void OnDisable()
    {
        EventManager.StopListening(typeof(ShakePuzzleEvent), OnShake);
        EventManager.StopListening(typeof(GeneratePuzzleEvent), OnGenerate);
        EventManager.StartListening(typeof(GarbageAcquireEvent), OnGarbageAcquire);
    }

    private void OnShake(IEvent param)
    {
        GetComponentsInChildren<Rigidbody2D>().ToList().ForEach((rigidBody2d) => {
            rigidBody2d.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(5f, 10f)), ForceMode2D.Impulse);
        });
    }

    private void OnGenerate(IEvent param)
    {
        GameObject prefab = Resources.Load<GameObject>(elements.Pick());
        Instantiate<GameObject>(prefab, transform);
    }

    private void OnGarbageAcquire(IEvent param)
    {
        if( param is GarbageAcquireEvent garbageAcquireEvent )
        {
            GameObject prefab = Resources.Load<GameObject>(elements[garbageAcquireEvent.GetGarbageIdx()]);
            Instantiate<GameObject>(prefab, transform);
        }
    }
}
