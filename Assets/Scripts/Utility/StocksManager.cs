using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StocksManager : MonoBehaviour
{
    public static int countDays;
    public float time;
    public List<Traders> traders;
    public static Func<float, string> IsThereAnyMoney;
    public static Dictionary<string, Vector3> positions = new Dictionary<string, Vector3>();
    public List<Transform> stands = new List<Transform>();
    public static Dictionary<string, Traders> dTr = new Dictionary<string, Traders>();
    public TextMeshProUGUI timer;
    public bool startGame = true;
    public List<Traders> rating = new List<Traders>();
    private void Awake()
    {
        rating = traders;
        foreach(var item in traders)
        {
            dTr.Add(item.nameTrader, item);
        }
        for (int i = 0; i < traders.Count; i++)
        {
            if (stands[i].transform.gameObject.GetComponent<TradersNPC>())
                stands[i].transform.gameObject.GetComponent<TradersNPC>().me = traders[i];
            else
                stands[i].transform.gameObject.GetComponent<MicrophoneInput>().me = traders[i];
            positions.Add(traders[i].nameTrader, stands[i].position);
        }
        IsThereAnyMoney += ChekerMoney;
    }
    public void EndDay()
    {
        foreach(var trader in traders)
        {
            trader.UpDownCost();
        }
        foreach (var trader in traders)
        {
            trader.GivePrecents();
        }
    }
    public string ChekerMoney(float money)
    {
        FisherYatesShuffle(traders);
        Traders mostPrior = null;
        for (int i = 0; i < traders.Count; i++)
        {
            if (!mostPrior || mostPrior.voiceStrenght < traders[i].voiceStrenght)
            {
                mostPrior = traders[i];
            }
        }
        if (mostPrior.costOneStock - mostPrior.voiceStrenght <= money)
        {
            return mostPrior.nameTrader;
        }
        return "";
    }
    static void FisherYatesShuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;

        // Start from the end and swap elements with a random one
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    static List<Traders> BubbleSort(List<Traders> array)
    {
        var len = array.Count;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                if (array[j].voiceStrenght > array[j + 1].voiceStrenght)
                {
                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }

        return array;
    }
    private void Update()
    {
        if (time > 0 && startGame)
        {
            time -= Time.deltaTime;
            timer.text = Mathf.RoundToInt(time).ToString();
        }
        if (time <= 0 && startGame)
        {
            startGame = false;
            timer.text = 0.ToString();
            foreach (var item in GameObject.FindGameObjectsWithTag("NPC"))
            {
                Destroy(item.gameObject);
            }
            foreach(var k in traders)
            {
                k.limitToSell += Mathf.RoundToInt(k.countBuyToday * 0.5f);
                k.UpDownCost();
            }
            foreach (var k in traders)
            {
                k.GivePrecents();
            }
        }
        // Debug
        rating = BubbleSort(rating);
        rating.Reverse();
    }
}
