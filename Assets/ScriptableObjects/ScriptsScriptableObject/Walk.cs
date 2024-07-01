using UnityEngine;

[CreateAssetMenu(fileName = "Walk", menuName = "Scriptable Objects/Walk")]
public class Walk : Stating
{
    public float radius;
    public float time;
    public float maxTime;
    public Stating idle, buy;
    public override void Init()
    {
        IsFinished = false;
        return;
    }
    public override void Doing()
    {
        if (Vector2.Distance(npc.transform.position, npc.posOfStation) <= radius) 
        {
            npc.SetState(buy);
            return;
        }
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            npc.SetState(idle);
            return;
        }
        npc.MoveToStand();
    }
}
