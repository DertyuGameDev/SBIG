using UnityEngine;
using UnityEngine.UI;
public class ScrollCalculate : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel, sampleListItem;

    public HorizontalLayoutGroup HLG;

    public bool IsSnaped;
    public float snapForce, snapSpeed;
    public GameObject[] items;
    public float lerp;
    private void Start()
    {
        for (int i = 0; i < contentPanel.childCount; i++)
        {
            items[i] = contentPanel.GetChild(i).gameObject;
        }
        sampleListItem = items[0].GetComponent<RectTransform>();
    }
    void Update()
    {
        int current = Mathf.RoundToInt(0 - contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing));
        for (int i = 0; i < items.Length; i++)
        {
            if (i == current)
            {
                items[current].transform.localScale = Vector3.Lerp(items[current].transform.localScale, Vector3.one, lerp);
            }
            else
            {
                items[i].transform.localScale = Vector3.Lerp(items[current].transform.localScale, new Vector3(0.7f, 0.7f, 0.7f), lerp);
            }
        }
        if (scrollRect.velocity.magnitude < 200 && !IsSnaped)
        {
            scrollRect.velocity = Vector3.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanel.localPosition.x, 
                0 - (current * sampleListItem.rect.width + HLG.spacing), snapSpeed), 
                contentPanel.localPosition.y, contentPanel.localPosition.z);
            if (contentPanel.localPosition.x == 0 - (current * sampleListItem.rect.width + HLG.spacing))
            {
                IsSnaped = true;
            }
        }
        if (scrollRect.velocity.magnitude > 200)
        {
            IsSnaped = false;
            snapSpeed = 0;
        }
    }
}
