using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler
{
    public Canvas canvas;
    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        float width = rect.rect.width;
        float height = rect.rect.height;
        rect.anchoredPosition = new Vector2(Mathf.Clamp(rect.anchoredPosition.x, -width, width), 
            Mathf.Clamp(rect.anchoredPosition.y, -height, height));
    }
}
