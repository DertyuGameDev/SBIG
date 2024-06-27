using UnityEngine;

[CreateAssetMenu(fileName = "Bonus", menuName = "Scriptable Objects/Bonus")]
public class Bonus : ScriptableObject
{
    public int cost;
    public float bonusToVoice;
    public string Name;
    public Sprite sprite;
}
