using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public interface PuzzleElementRemover
{
    void RegisterDeleteCandidate(PuzzleElement contacted);
    void UnregisterDeleteCandidate(PuzzleElement contacted);
    void RemoveCandidates();
}

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PuzzleElement : MonoBehaviour, IPointerClickHandler
{
    public PuzzleElementRemover remover = null;
    // private List<PuzzleElement> contactedGameObjects = new List<PuzzleElement>();

    private SpriteRenderer spriteRenderer = null;
    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // this.spriteRenderer.color = this.contactedGameObjects.Count > 0 ? Color.green : Color.white;
    }

    void OnCollisionStay2D(Collision2D collision2d)
    {
        if ( gameObject.name == collision2d.gameObject.name )
        {
            // var puzzleElement = collision2d.collider.gameObject.GetComponent<PuzzleElement>();
            // if ( puzzleElement != null && contactedGameObjects.Select((contacted) => contacted == puzzleElement).Count() == 0 )
            // {
            //     contactedGameObjects.Add(puzzleElement);
            // }
            
        }
    }

    void OnCollisionExit2D(Collision2D collision2d)
    {
        if ( gameObject.name == collision2d.gameObject.name )
        {
            // var puzzleElement = collision2d.collider.gameObject.GetComponent<PuzzleElement>();
            // if ( puzzleElement != null )
            // {
            //     contactedGameObjects.Remove(puzzleElement);
            // }
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if( this.contactedGameObjects.Count == 0 )
        {
            return;
        }
        else
        {
            if ( remover.RegisterDeleteCandidates(this.contactedGameObjects) )
            {
                this.contactedGameObjects.ForEach((puzzleElement) => puzzleElement.OnPointerClick(pointerEventData));
            }
            else
            {
                remover.RemoveCandidates();
            }
            
        }
    }
}
