using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonusAttributes
{
    public int state;
    public int cost;
    public string nameBonus;
    public Bonus bonus;
}

[CreateAssetMenu(fileName = "BuyBonusStocks", menuName = "Scriptable Objects/BuyBonusStocks")]
public class BuyBonusStocks : StatingTraders
{
    public float money;
    public List<BonusAttributes> bonuses = new List<BonusAttributes>();
    public override void Init()
    {
        money = PlayerPrefs.GetFloat("Money" + trader.me.nameTrader, 0);
        foreach (BonusAttributes attr in bonuses)
        {
            attr.state = PlayerPrefs.GetInt(attr.nameBonus + trader.me.nameTrader, 0);
        }
    }
    public override void Doing()
    {
        if (money > 0)
        {
            foreach (BonusAttributes attr in bonuses)
            {
                if (trader.bonus != null)
                {
                    if (attr.state == 0 && attr.bonus.bonusToVoice > trader.bonus.bonusToVoice)
                    {
                        if (money - attr.cost >= 0)
                        {
                            PlayerPrefs.SetFloat("Money" + trader.me.nameTrader, money - attr.cost);
                            money = PlayerPrefs.GetFloat("Money" + trader.me.nameTrader, 0);
                            attr.state = 2;
                            PlayerPrefs.SetString("NowBonus" + trader.me.nameTrader, attr.nameBonus);
                            trader.bonus = attr.bonus;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (attr.state == 0)
                    {
                        if (money - attr.cost >= 0)
                        {
                            PlayerPrefs.SetFloat("Money" + trader.me.nameTrader, money - attr.cost);
                            money = PlayerPrefs.GetFloat("Money" + trader.me.nameTrader, 0);
                            attr.state = 2;
                            PlayerPrefs.SetString("NowBonus" + trader.me.nameTrader, attr.nameBonus);
                            trader.bonus = attr.bonus;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        BestBuyManager.bestSell(trader.me);
        if (money > 0)
        {
            BestBuyManager.buysBestPrecent(trader.me.nameTrader);
        }
        foreach(BonusAttributes attr in bonuses)
        {
            if (attr.state == 2 && PlayerPrefs.GetString("NowBonus" + trader.me.nameTrader, "") != attr.nameBonus)
            {
                trader.bonus = null;
                attr.state = 1;
            }
            if (attr.state == 2 && PlayerPrefs.GetString("NowBonus" + trader.me.nameTrader, "") == attr.nameBonus)
            {
                trader.bonus = attr.bonus;
            }
        }
        IsFinished = true;
    }
}
