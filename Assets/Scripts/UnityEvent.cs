using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
    public UnityEvent onInteract; 
    public LayerMask interactableLayer; 
    public float interactionRange = 1f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
        foreach (var interactable in interactables)
        {
            if (interactable.CompareTag("Interactable"))
            {
                Debug.Log("Взаимодействие с объектом: " + interactable.name);
                onInteract.Invoke(); 
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}