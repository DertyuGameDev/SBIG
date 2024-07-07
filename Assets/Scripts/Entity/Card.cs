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
        if (PlayerPrefs.GetFloat("Money" + trader.nameTrader, 0) - bonus.cost >= 0 && state == State.BUY)
        {
            PlayerPrefs.SetFloat("Money" + trader.nameTrader, PlayerPrefs.GetFloat("Money" + trader.nameTrader, 0) - bonus.cost);
            PlayerPrefs.SetString("NowBonus" + trader.nameTrader, nameText.text);
            PlayerPrefs.SetInt(nameText.text, 2);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("NowBonusGosha", "") == nameText.text)
        {
            if (MicrophoneInput.bonus == null)
            {
                MicrophoneInput.bonus = bonus;
            }
            else
            {
                if (MicrophoneInput.bonus.Name != "Ulta")
                {
                    MicrophoneInput.bonus = bonus;
                }
            }
        }
        if (state == State.SELECTED && PlayerPrefs.GetString("NowBonusGosha", "") != nameText.text) 
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
