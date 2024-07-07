using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellBuy : MonoBehaviour
{
    public Traders me;
    float precent;
    float freeToBuy;
    public Slider slider, buysSlider;
    public TextMeshProUGUI textBuys, textSell;
    public TextMeshProUGUI butTextSell, butTextBuy;
    public void Text()
    {
        textBuys.text = buysSlider.value.ToString() + "%";
        textSell.text = slider.value.ToString() + "%";
        butTextSell.text = string.Format("+{0:f}$", StocksManager.MainPerson.SellPrice(me.nameTrader, slider.value));
        butTextBuy.text = string.Format("{0:f}$", me.countStocksNow / 100 * buysSlider.value * me.costOneStock);
    }
    public void Setup(Traders traders)
    {
        me = traders;
        if (StocksManager.MainPerson.precentsOfOther.Keys.Contains(me.nameTrader))
        {
            foreach (var trader in StocksManager.MainPerson.precentsOfOther[me.nameTrader].Keys)
            {
                precent = trader;
            }
        }
        else
        {
            slider.value = 0;
        }
        if (precent < 1)
        {
            slider.value = 0;
        }
        freeToBuy = BestBuyManager.getPrecent(me.nameTrader);
        if (freeToBuy == 0)
        {
            slider.value = 0;
        }
        slider.maxValue = precent;
        buysSlider.maxValue = freeToBuy;
    }
    public void Sell()
    {
        StocksManager.MainPerson.SellPrecents(me.nameTrader, slider.value);
    }
    public void Buy()
    {
        if (PlayerPrefs.GetFloat("MoneyGosha", 0) - me.countStocksNow / 100 * buysSlider.value * me.costOneStock >= 0)
        {
            PlayerPrefs.SetFloat("MoneyGosha", PlayerPrefs.GetFloat("MoneyGosha", 0) - me.countStocksNow / 100 * buysSlider.value * me.costOneStock);
            StocksManager.MainPerson.BuyPrecents(me, buysSlider.value);
        }
        StocksManager.MainPerson.CheckWin();
    }
    private void Update()
    {
        if (StocksManager.MainPerson.precentsOfOther.Keys.Contains(me.nameTrader))
        {
            foreach (var trader in StocksManager.MainPerson.precentsOfOther[me.nameTrader].Keys)
            {
                precent = trader;
            }
        }
        else
        {
            slider.value = 0;
        }
        if (precent < 1)
        {
            slider.value = 0;
        }
        freeToBuy = BestBuyManager.getPrecent(me.nameTrader);
        if (freeToBuy == 0)
        {
            slider.value = 0;
        }
        slider.maxValue = precent;
        buysSlider.maxValue = freeToBuy;
        Text();
    }
}
