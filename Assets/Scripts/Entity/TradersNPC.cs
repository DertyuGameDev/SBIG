using System.Collections.Generic;
using UnityEngine;

public class TradersNPC : MonoBehaviour
{
    public AudioSource source;
    public float result;
    [Header("Attribute Settings")]
    public StatingTraders scream;
    public StatingTraders buyStockBonus;

    public StatingTraders currentState;
    public Traders me;
    public Bonus bonus = null;
    private void Awake()
    {
        SetState(scream);
    }
    private void Update()
    {
        if (currentState.IsFinished == false)
        {
            currentState.Doing();
        }
        else
        {
            if (source.enabled)
            {
                SetState(scream);
            }
        }
    }
    public void SetState(StatingTraders state)
    {
        currentState = Instantiate(state);
        currentState.trader = this;
        currentState.Init();
    }
}
