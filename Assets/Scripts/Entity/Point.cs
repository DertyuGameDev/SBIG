using UnityEngine;
using TMPro;

public class Point : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Gradient green, red, yellow;
    public void Ordinate()
    {
        text.text = (transform.position.y * 1000).ToString();
    }
    public void Update()
    {
        Ordinate();
    }
    public void SetupLine(Vector3 myPos, Vector3 posNext)
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        Vector3[] m = new Vector3[] {myPos, posNext};
        line.SetPositions(m);
        if (myPos.y > posNext.y)
        {
            line.colorGradient = red;
        }
        else if (myPos.y < posNext.y)
        {
            line.colorGradient = green;

        }
        else
        {
            line.colorGradient = yellow;
        }
    }
}
