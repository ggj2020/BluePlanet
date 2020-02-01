using UnityEngine;
using System.Linq;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class PuzzleElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshPro textMeshPro = null;

    private GameObject _contactedGameObject;
    private GameObject contactedGameObject
    {
        get
        {
            return this._contactedGameObject;
        }
        set 
        {
            this._contactedGameObject = value;
            this.textMeshPro.text = (value != null ? "TOUCH" : "");
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
