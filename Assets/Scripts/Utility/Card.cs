using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum State
{
    BUY,
    SELECT,
    SELECTED
}

public class Card : MonoBehaviour
{
    public Bonus bonus;
    public Image sprite;
    public TextMeshProUGUI nameText;
    public Button buy;
    public TextMeshProUGUI costText;
    public State state;

    public void Buy(Traders trader)
    {
        if (state == State.SELECT)
        {
            PlayerPrefs.SetString("NowBonus" + trader.nameTrader, nameText.text);
            PlayerPrefs.SetInt(nameText.text, 2);
            return;
        }
        if (state == State.SELECTED)
        {
            return;
        }
        if (PlayerPrefs.GetInt("Money" + trader.nameTrader, 0) - Convert.ToInt32(costText.text.Substring(0, costText.text.Length - 2)) >= 0 
            && state == State.BUY)
        {
            PlayerPrefs.SetInt("Money" + trader.nameTrader, PlayerPrefs.GetInt("Money" + trader.nameTrader, 0) - Convert.ToInt32(costText.text.Substring(0, costText.text.Length - 2)));
            PlayerPrefs.SetString("NowBonus" + trader.nameTrader, nameText.text);
            PlayerPrefs.SetInt(nameText.text, 2);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("NowBonusTraders", "") != "" && PlayerPrefs.GetString("NowBonusTraders") == nameText.text)
        {
            MicrophoneInput.bonus = bonus;
        }
        if (state == State.SELECTED && PlayerPrefs.GetString("NowBonusTraders") != nameText.text) 
        {
            PlayerPrefs.SetInt(nameText.text, 1);
        }

        switch (PlayerPrefs.GetInt(nameText.text, 0))
        {
            case 0:
                state = State.BUY;
                break;
            case 1:
                state = State.SELECT;
                costText.text = "Select";
                break;
            case 2:
                state = State.SELECTED;
                costText.text = "Selected";
                break;
        }
    }
}
