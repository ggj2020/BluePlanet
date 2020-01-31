using UnityEngine;
using System.Linq;
using System.Collections;

public class PuzzleElement : MonoBehaviour
{
    static private float contactReDetectSeconds = 2f;
    void OnCollisionStay2D(Collision2D collision2d)
    {
        if ( gameObject.name == collision2d.gameObject.name )
        {
            StartCoroutine(DetectContactAfterFewSeconds(collision2d.collider, collision2d.otherCollider));
        }
    }

    private IEnumerator DetectContactAfterFewSeconds(Collider2D collider, Collider2D otherCollider)
    {
        yield return new WaitForSeconds(contactReDetectSeconds);
        if ( collider != null && otherCollider != null && collider.Distance(otherCollider).distance <= 0 )
        {
            DestroyGameObjects(collider.gameObject, otherCollider.gameObject);
        }
    }

    private void DestroyGameObjects(params GameObject[] gameObjects)
    {
        gameObjects.ToList().ForEach(Destroy);
    }
}
