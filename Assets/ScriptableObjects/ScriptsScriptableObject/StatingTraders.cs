using UnityEngine;

[CreateAssetMenu(fileName = "StatingTraders", menuName = "Scriptable Objects/StatingTraders")]
public class StatingTraders : ScriptableObject
{
    public bool IsFinished = false;
    public TradersNPC trader;
    public virtual void Init() { }
    public virtual void Doing() { }
}
