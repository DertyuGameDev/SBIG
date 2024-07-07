using UnityEngine;
using TMPro;
public class MoneyTMP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    void Update()
    {
        text.text = string.Format("{0:f}$ - My free precent: {1}%", PlayerPrefs.GetFloat("MoneyGosha", 0), BestBuyManager.getPrecent("Gosha"));
    }
}
