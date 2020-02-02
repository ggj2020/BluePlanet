using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PuzzleElementGenerator : MonoBehaviour, PuzzleElementRemover
{

    private string[] elements = new string[] {
        "Puzzle/Prefabs/Pentagon",
        // "Puzzle/Prefabs/RoundedRectangle",
        // "Puzzle/Prefabs/Triangle"
    };

    private List<PuzzleElement> removeCandidates = new List<PuzzleElement>();

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
        var garbage = Instantiate<GameObject>(prefab, transform);
        garbage.GetComponent<PuzzleElement>().remover = this;
    }

    private void OnGarbageAcquire(IEvent param)
    {
        if( param is GarbageAcquireEvent garbageAcquireEvent )
        {
            //GameObject prefab = Resources.Load<GameObject>(elements[garbageAcquireEvent.GetGarbageIdx()]);
            GameObject prefab = Resources.Load<GameObject>(elements[0]);
            var garbage = Instantiate<GameObject>(prefab, transform);
            garbage.GetComponent<PuzzleElement>().remover = this;
        }
    }

    public bool RegisterDeleteCandidates(List<PuzzleElement> contanctedGameObjects)
    {
        var changedCandidate = false;   
        foreach( var contactedGameObject in contanctedGameObjects )
        {
            var foundedInCandidate = false;
            foreach( var candidate in removeCandidates )
            {
                if ( candidate == contactedGameObject )
                {
                    foundedInCandidate = true;
                    break;
                }
            }

            if ( foundedInCandidate == false )
            {
                removeCandidates.Add(contactedGameObject);
                changedCandidate = true;
            }
        }
        return changedCandidate;
    }
    public void RemoveCandidates()
    {
        removeCandidates.ForEach(Destroy);
        removeCandidates.Clear();
    }

    void RegisterDeleteCandidate(PuzzleElement contacted)
    {

    }
    void UnregisterDeleteCandidate(PuzzleElement contacted)
    {
    }
}
