using UnityEngine;


[CreateAssetMenu(fileName = "Stating", menuName = "Scriptable Objects/Stating")]
public abstract class Stating : ScriptableObject
{
    public bool IsFinished = false;
    public NPC npc;
    public virtual void Init() { }
    public virtual void Doing() { }
}
