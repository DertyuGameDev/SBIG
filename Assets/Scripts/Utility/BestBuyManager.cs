using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BestBuyManager : MonoBehaviour
{
    public List<float> rate = new List<float>();
    public List<string> names = new List<string>();
    public List<Traders> traders = new List<Traders>();
    public static Action<string> buysBestPrecent;
    public static Func<string, float> getPrecent;
    public static Action<Traders> bestSell;

    private void Awake()
    {
        buysBestPrecent += Buy;
        getPrecent += GetFreePrecents;
        bestSell += Sell;
    }
    public void Buy(string nameTrader)
    {
        if (Random.Range(0, 100) < 10)
        {
            print("ok");
            return;
        }
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
        Traders me = null;
        List<Traders> other = new List<Traders>();
        List<Traders> other1;
        foreach (Traders trader in traders)
        {
            if (trader.nameTrader == nameTrader)
            {
                me = trader;
                continue;
            }
            other.Add(trader);
        }
        other = BubbleSort1(other, true);
        other.Reverse();
        other1 = other;
        other1 = BubbleSort1(other1, false);
        other1.Reverse();
        int u = 2;
        foreach(var r in other1)
        {
            if (keyValuePairs.ContainsKey(r.nameTrader))
            {
                keyValuePairs[r.nameTrader] += u;
                u -= 1;
            }
            else
            {
                keyValuePairs.Add(r.nameTrader, u);
                u -= 1;
            }
        }
        u = 2;
        foreach (var r in other1)
        {
            keyValuePairs[r.nameTrader] += u;
            u -= 1;
        }
        string n = "";
        foreach (var k in keyValuePairs.Keys)
        {
            if (n == "")
            {
                n = k;
                continue;
            }
            if (keyValuePairs[n] < keyValuePairs[k])
            {
                n = k;
            }
        }
        foreach(var r in keyValuePairs.Keys)
        {
            names.Add(r);
            rate.Add(keyValuePairs[r]);
        }
        print(nameTrader);
        foreach(var r in keyValuePairs)
        {
            print(r.Key + "   " + r.Value);
        }
        foreach(var k in other)
        {
            if (k.nameTrader == n)
            {
                for (var i = 0; i < getPrecent(n) + 1; i++)
                {
                    if (PlayerPrefs.GetFloat("Money" + me.nameTrader, 0) - k.countStocksNow / 100 * i * k.costOneStock >= 0)
                    {
                        continue;
                    }
                    else if (i != 0)
                    {
                        PlayerPrefs.SetFloat("Money" + me.nameTrader, PlayerPrefs.GetFloat("Money" + me.nameTrader, 0) - k.countStocksNow / 100 * (i - 1) * k.costOneStock);
                        me.BuyPrecents(k, i - 1);
                        return;
                    }
                    else if (i == getPrecent(n))
                    {
                        PlayerPrefs.SetFloat("Money" + me.nameTrader, PlayerPrefs.GetFloat("Money" + me.nameTrader, 0) - k.countStocksNow / 100 * (i - 1) * k.costOneStock);
                        me.BuyPrecents(k, i - 1);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
    public void Sell(Traders tr)
    {
        foreach(var item in tr.precentsOfOther.Keys)
        {
            foreach(var t in tr.precentsOfOther[item].Keys)
            {
                float res = tr.SellPrice(item, t);
                float other = tr.precentsOfOther[item][t].total * tr.precentsOfOther[item][t].costOneStock;
                if (res > other * 3)
                {
                    tr.SellPrecents(item, t);
                }
                break;
            }
        }
    }
    public List<Traders> BubbleSort1(List<Traders> array, bool count)
    {
        for (var i = 1; i < array.Count; i++)
        {
            for (var j = 0; j < array.Count - i; j++)
            {
                float first, second;
                if (count)
                {
                    first = array[j].countStocksNow * array[j].costOneStock;
                    second = array[j + 1].countStocksNow * array[j + 1].costOneStock;
                }
                else
                {
                    first = array[j].stocksInfo[array[j].stocksInfo.Count - 1];
                    second = array[j + 1].stocksInfo[array[j + 1].stocksInfo.Count - 1];
                }
                if (first > second)
                {
                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }

        return array;
    }
    public float GetFreePrecents(string nameTr)
    {
        float p = 0;
        foreach (var item in traders)
        {
            foreach (var t in item.precentsOfOther.Keys)
            {
                if (t == nameTr)
                {
                    foreach(var y in item.precentsOfOther[t].Keys)
                    {
                        p += y;
                        break;
                    }
                    break;
                }
            }
        }
        return 100 - p;
    }
}
