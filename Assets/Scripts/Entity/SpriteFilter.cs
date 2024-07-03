using UnityEngine;

public class SpriteFilter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        collider2D.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
    }
}
