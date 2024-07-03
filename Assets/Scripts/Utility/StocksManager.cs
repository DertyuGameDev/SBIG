using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StocksManager : MonoBehaviour
{
    public static Traders MainPerson;
    public static int countDays;
    public static float moneyStart = 0;
    public static Func<float, string> IsThereAnyMoney;
    public static Dictionary<string, Transform> positions = new Dictionary<string, Transform>();
    public static Dictionary<string, Traders> dTr = new Dictionary<string, Traders>();

    public GameObject spawner;
    public Animator animator, stock;
    public TextMeshProUGUI timer;
    public bool startGame = true;
    public float time;

    public List<TradersNPC> npcList = new List<TradersNPC>();
    public List<Traders> rating = new List<Traders>();
    public List<float> moneys = new List<float>();
    public List<Traders> traders;
    public List<Transform> stands = new List<Transform>();
    private void Awake()
    {
        foreach (var item in traders)
        {
            PlayerPrefs.SetFloat("Money" + item.nameTrader, 0);
            if (item.nameTrader == "Gosha")
            {
                MainPerson = item;
            }
            moneys.Add(0);
        }
        for (int i = 0; i < traders.Count; i++)
        {
            for (int j = 0; j < traders.Count; j++)
            {
                if (traders[i] != traders[j])
                {
                    traders[i].BuyPrecents(traders[j], 5);
                }
            }
        }
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
            positions.Add(traders[i].nameTrader, stands[i]);
        }
        IsThereAnyMoney += ChekerMoney;
    }
    public void EndDay()
    {
        foreach(var trader in traders)
        {
            trader.limitToSell += Mathf.RoundToInt(trader.countBuyToday * 0.5f);
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
    public void Update()
    {
        foreach (var item in traders)
        {
            item.precents.Clear();
        }
        for (int i = 0;i < moneys.Count; i++)
        {
            moneys[i] = PlayerPrefs.GetFloat("Money" + traders[i].nameTrader, 0);
        }
        if (time > 0 && startGame)
        {
            animator.SetBool("Open", false);
            time -= Time.deltaTime;
            timer.text = Mathf.RoundToInt(time).ToString();
        }
        if (time <= 0 && startGame)
        {
            EndDay();
            foreach (var k in npcList)
            {
                k.gameObject.GetComponent<AudioSource>().enabled = false;
                k.SetState(k.buyStockBonus);
            }
            startGame = false;
            timer.text = 0.ToString();
            spawner.SetActive(false);
            stock.SetBool("Open", true);
            animator.SetBool("Open", true);
            foreach (var item in GameObject.FindGameObjectsWithTag("NPC"))
            {
                Destroy(item.gameObject);
            }
            //foreach (var trader in traders)
            //{
            //    trader.ShowPrecent();
            //}
        }
        // Debug
        rating = BubbleSort(rating);
        rating.Reverse();
    }
}
