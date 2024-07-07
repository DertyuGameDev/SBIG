using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Traders", menuName = "Scriptable Objects/Traders")]
public class Traders : ScriptableObject
{
    public float money;
    public Sprite MyPortret;

    public string nameTrader;

    public float costOneStock;
    public float countStocksNow;

    public bool main;
    [Header("Counts")]
    public int countBuyToday;
    public int countSellToday;
    [Header("Debug")]
    public int limitToSell = 10;
    public float total;
    public float upCost;
    public float voiceStrenght;
    public List<float> precents = new List<float>();
    public List<float> stocksInfo = new List<float>();
    public Dictionary<string, Dictionary<float, Traders>> precentsOfOther = new Dictionary<string, Dictionary<float, Traders>>();

    public void UpDownCost()
    {
        if (countStocksNow > 0)
        {
            if (countStocksNow < limitToSell)
            {
                countSellToday = Random.Range(0, countBuyToday);
            }
            else
            {
                countSellToday = Random.Range(0, limitToSell);
            }
        }
        else
        {
            countSellToday = 0;
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
        float g = total * costOneStock;
        float freePrecent = BestBuyManager.getPrecent(nameTrader);
        if ((costOneStock <= 0 || freePrecent == 0) && nameTrader == "Gosha")
        {
            StocksManager.loseAction();
        }
        stocksInfo[stocksInfo.Count - 1] -= g * (100 - freePrecent) / 100;
        PlayerPrefs.SetFloat("Money" + nameTrader, PlayerPrefs.GetFloat("Money" + nameTrader, 0) + g * freePrecent / 100);
        int y = 0;
        if (precentsOfOther.Keys.ToList().Count > 1 && nameTrader == "Gosha")
        {
            foreach (var item in precentsOfOther)
            {
                foreach (var it in item.Value.ToList())
                {
                    if (BestBuyManager.getPrecent(it.Value.nameTrader) == 0)
                    {
                        y += 1;
                    }
                }
            }
        }
        if (y >= 2 && nameTrader == "Gosha")
        {
            StocksManager.winAction();
        }
    }
    public void BuyPrecents(Traders trader, float precent)
    {
        float p = 0;
        if (precentsOfOther.ContainsKey(trader.nameTrader))
        {
            foreach (var item in precentsOfOther[trader.nameTrader].Keys.ToList())
            {
                p = item;
            }
            precentsOfOther[trader.nameTrader] = new Dictionary<float, Traders> { { precent + p, trader } };
            return;
        }
        else
        {
            precentsOfOther.Add(trader.nameTrader, new Dictionary<float, Traders> { { precent + p, trader } });
        }
    }
    public void CheckWin()
    {
        int y = 0;
        if (precentsOfOther.Keys.ToList().Count > 1 && nameTrader == "Gosha")
        {
            foreach (var item in precentsOfOther)
            {
                foreach (var it in item.Value.ToList())
                {
                    if (BestBuyManager.getPrecent(it.Value.nameTrader) == 0)
                    {
                        y += 1;
                    }
                }
            }
        }
        if (y >= 2 && nameTrader == "Gosha")
        {
            StocksManager.winAction();
        }
    }
    public float SellPrice(string nameT, float precent)
    {
        if (precentsOfOther.Keys.Contains(nameT))
        {
            Dictionary<float, Traders> d = precentsOfOther[nameT];
            foreach (var k in d.Keys.ToList())
            {
                Traders tr = d[k];
                return tr.countStocksNow * tr.costOneStock * precent / 100;
            }
        }
        return 0;
    }
    public int SellPrecents(string nameT, float precent)
    {
        if (precentsOfOther.Keys.Contains(nameT))
        {
            Dictionary<float, Traders> d = precentsOfOther[nameT];
            foreach (var k in d.Keys.ToList()) 
            {
                float pr = k;
                Traders tr = d[k];
                if (pr > precent)
                {
                    d.Remove(pr);
                    d.Add(pr - precent, tr);
                    precentsOfOther[nameT] = d;
                    PlayerPrefs.SetFloat("Money" + nameTrader, PlayerPrefs.GetFloat("Money" + nameTrader, 0) + tr.countStocksNow * tr.costOneStock * precent / 100);
                    return 1;
                }
                else if (pr == precent)
                {
                    precentsOfOther.Remove(nameT);
                    PlayerPrefs.SetFloat("Money" + nameTrader, PlayerPrefs.GetFloat("Money" + nameTrader, 0) + tr.countStocksNow * tr.costOneStock * pr / 100);
                    return 1;
                }
            }
        }
        return 0;
    }
    public void ShowPrecent()
    {
        foreach (var keys in precentsOfOther.Keys.ToList())
        {
            Dictionary<float, Traders> d = precentsOfOther[keys];
            foreach (var key2 in d.Keys.ToList())
            {
                Traders tr = d[key2];
                float pr = key2;
                Debug.Log(nameTrader + " : " + tr.nameTrader + " precents " + pr + " in money - " + tr.total * tr.costOneStock * pr / 100);
                Debug.Log(nameTrader + " : " + PlayerPrefs.GetFloat("Money" + nameTrader, 0));
            }
        }
    }
    public void GivePrecents()
    {
        foreach(var keys in precentsOfOther.Keys.ToList())
        {
            Dictionary<float, Traders> d = precentsOfOther[keys];
            foreach (var key2 in d.Keys.ToList())
            {
                Traders tr = d[key2];
                float pr = key2;
                //Debug.Log(nameTrader + " : " + tr.nameTrader + " precents " + pr + " in money - " + tr.total * tr.costOneStock * pr / 100);
                //Debug.Log(nameTrader + " : " + PlayerPrefs.GetFloat("Money" + nameTrader, 0));
                PlayerPrefs.SetFloat("Money" + nameTrader, PlayerPrefs.GetFloat("Money" + nameTrader, 0) + tr.total * tr.costOneStock * pr / 100);
            }
        }
    }
}
