using UnityEngine;
using TMPro;

public class Point : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void Ordinate()
    {
        text.text = transform.position.y.ToString();
    }
    public void Update()
    {
        Ordinate();
    }
}
