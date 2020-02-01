using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PuzzleElement : MonoBehaviour, IPointerClickHandler
{
    private GameObject _contactedGameObject;
    private GameObject contactedGameObject
    {
        get
        {
            return this._contactedGameObject;
        }
        set 
        {
            var material = GetComponent<SpriteRenderer>().material;
            material.SetFloat("_OutlineBrightness", value == null ? 0f : 2.8f);
            material.SetFloat("_OutlineWidth", value == null ? 0f : 0.0156f);
            this._contactedGameObject = value;
        }
    }

    void Start()
    {
        this.contactedGameObject = null;
    }

    void OnCollisionStay2D(Collision2D collision2d)
    {
        if ( gameObject.name == collision2d.gameObject.name )
        {
            this.contactedGameObject = collision2d.collider.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision2d)
    {
        if ( gameObject.name == collision2d.gameObject.name )
        {
            this.contactedGameObject = null;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if( this.contactedGameObject == null )
        {
            return;
        }
        else if ( this.contactedGameObject.name == gameObject.name )
        {
            DestroyGameObjects(gameObject, this.contactedGameObject);
        }
    }

    private void DestroyGameObjects(params GameObject[] gameObjects)
    {
        gameObjects.ToList().ForEach(Destroy);
    }
}
