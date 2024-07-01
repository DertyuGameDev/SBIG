using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class StockCard : MonoBehaviour
{
    [Header("UI")]
    public Image portret;
    public TextMeshProUGUI precents, up;
    public Button graph;
    [Header("Attributes")]
    public Traders me;
    void Update()
    {
        portret.sprite = me.MyPortret;
        float g = 0;
        if (StocksManager.MainPerson.precentsOfOther.Keys.Contains(me.nameTrader))
        {
            foreach (var it in StocksManager.MainPerson.precentsOfOther[me.nameTrader].Keys)
            {
                g = it;
            }
            precents.text = string.Format("Your precent: {0:f}%", g);
        }
        else
        {
            precents.text = "Your precent: 0%";
        }
        float u = me.upCost;
        if (u > 0)
        {
            up.color = Color.green;
            up.text = string.Format("+{0:f}$", u);
        }
        else if (u == 0)
        {
            up.color = Color.yellow;
            up.text = "0$";
        }
        else
        {
            up.color = Color.red;
            up.text = string.Format("-{0:f}$", u);
        }
    }
}
