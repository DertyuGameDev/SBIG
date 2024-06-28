using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Traders", menuName = "Scriptable Objects/Traders")]
public class Traders : ScriptableObject
{
    public string nameTrader;

    public float costOneStock;

    public bool main;
    [Header("Counts")]
    public int countStocksNow;
    public int countBuyToday;
    public int countSellToday;
    [Header("Debug")]
    public int limitToSell = 10;
    [HideInInspector] public int total;
    [HideInInspector] public float upCost;
    [HideInInspector] public float voiceStrenght;
    public List<float> stocksInfo = new List<float>();
    public Dictionary<string, Dictionary<float, Traders>> precentsOfOther = new Dictionary<string, Dictionary<float, Traders>>();

    public void UpDownCost()
    {
        if (countBuyToday > 0)
        {
            if (countSellToday < limitToSell)
            {
                countSellToday = Random.Range(0, countBuyToday);
            }
            else
            {
                countSellToday = Random.Range(0, limitToSell);
            }
        }
        total = countBuyToday - countSellToday;
        if (total > 0 && total > 50)
        {
            upCost = total / 2;
        }
        else if (total > 0 && total < 50)
        {
            upCost = total / 10;
        }
        else
        {
            upCost = total;
        }
        countStocksNow += total;
        costOneStock += upCost;
        stocksInfo.Add(countStocksNow * costOneStock);
        if (costOneStock <= 0)
        {
            // end
        }
    }
    public void BuyPrecents(Traders trader, float precent)
    {
        Dictionary<float, Traders> d = new Dictionary<float, Traders>();
        d.Add(precent, trader);
        precentsOfOther.Remove(trader.nameTrader);
        precentsOfOther.Add(trader.nameTrader, d);
    }
    public void GivePrecents()
    {
        foreach(var keys in precentsOfOther.Keys)
        {
            Dictionary<float, Traders> d = precentsOfOther[keys];
            foreach (var key2 in d.Keys)
            {
                Traders tr = d[key2];
                float pr = key2;
                PlayerPrefs.SetInt("Money" + nameTrader, PlayerPrefs.GetInt("Money" + nameTrader, 0) + Mathf.RoundToInt(tr.total * tr.costOneStock * pr / 100));
            }
        }
    }
}
