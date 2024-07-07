using UnityEngine;

public class NPCElevatorSpriteFilter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<SpriteRenderer>().sortingOrder = 4;
    }
}
