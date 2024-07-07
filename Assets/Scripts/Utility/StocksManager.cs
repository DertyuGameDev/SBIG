using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StocksManager : MonoBehaviour
{
    public static Traders MainPerson;
    public static int countDays;
    public static float moneyStart = 0;
    public static Action cont, loseAction, winAction;
    public static Func<float, string> IsThereAnyMoney;
    public static Dictionary<string, Transform> positions = new Dictionary<string, Transform>();
    public static Dictionary<string, Traders> dTr = new Dictionary<string, Traders>();

    public SpawnerNPC spawn;
    public PlayerController playerController;
    public GameObject lose, win;
    public Transform menu;
    public TextMeshProUGUI timer;
    public bool startGame = true;
    public float time;

    public List<TradersNPC> npcList = new List<TradersNPC>();
    public List<Traders> rating = new List<Traders>();
    public List<float> moneys = new List<float>();
    public List<Traders> traders;
    public List<Transform> stands = new List<Transform>();

    public void Lose()
    {
        Time.timeScale = 0;
        lose.gameObject.SetActive(true);
    }

    public void AudioEn(bool t)
    {
        foreach (var k in npcList)
        {
            k.gameObject.GetComponent<AudioSource>().enabled = t;
        }
    }

    public void Win()
    {
        Time.timeScale = 0;
        win.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        Ultra.start();
        startGame = true;
        time = 50;
        spawn.enabled = true;
        menu.DOLocalMoveY(-1165, 0.4f);
        playerController.enabled = true;
        playerController.transform.position = new Vector3(13.86f, -7.421f, 0);
        foreach (var k in npcList)
        {
            k.gameObject.GetComponent<AudioSource>().enabled = true;
            k.gameObject.GetComponent<AudioSource>().Play();
            k.SetState(k.scream);
        }
    }
    private void Awake()
    {
        loseAction += Lose;
        winAction += Win;
        cont += StartGame;
        foreach (var item in traders)
        {
            PlayerPrefs.SetFloat("Money" + item.nameTrader, 0);
            if (item.nameTrader == "Gosha")
            {
                MainPerson = item;
            }
            item.stocksInfo.Clear();
            item.stocksInfo.Add(0);
            item.limitToSell = 10;
            item.costOneStock = 10;
            item.countBuyToday = 0;
            item.countStocksNow = 0;
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
            if (!dTr.ContainsKey(item.nameTrader))
            {
                dTr.Add(item.nameTrader, item);
            }
        }
        for (int i = 0; i < traders.Count; i++)
        {
            if (stands[i].transform.gameObject.GetComponent<TradersNPC>())
                stands[i].transform.gameObject.GetComponent<TradersNPC>().me = traders[i];
            else
                stands[i].transform.gameObject.GetComponent<MicrophoneInput>().me = traders[i];
            if (!positions.ContainsKey(traders[i].nameTrader))
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
        foreach (var trader in traders)
        {
            trader.countBuyToday = 0;
            trader.countSellToday = 0;
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
        for (int i = 0; i < moneys.Count; i++)
        {
            moneys[i] = PlayerPrefs.GetFloat("Money" + traders[i].nameTrader, 0);
            traders[i].money = PlayerPrefs.GetFloat("Money" + traders[i].nameTrader, 0);
        }
        if (time > 0 && startGame)
        {
            time -= Time.deltaTime;
            timer.text = Mathf.RoundToInt(time).ToString();
        }
        if (time <= 0 && startGame)
        {
            Ultra.stop();
            EndDay();
            foreach (var k in npcList)
            {
                k.gameObject.GetComponent<AudioSource>().enabled = false;
                k.SetState(k.buyStockBonus);
            }
            menu.DOLocalMoveY(0, 0.4f);
            spawn.enabled = false;
            startGame = false;
            playerController.enabled = false;
            timer.text = 0.ToString();
            foreach (var item in GameObject.FindGameObjectsWithTag("NPC"))
            {
                Destroy(item.gameObject);
            }
            MainPerson.CheckWin();
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
