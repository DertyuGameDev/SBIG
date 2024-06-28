using UnityEngine;

[CreateAssetMenu(fileName = "WalkRandom", menuName = "Scriptable Objects/WalkRandom")]
public class WalkRandom : Stating
{
    public float radius;
    public Stating idle;
    public override void Init()
    {
        IsFinished = false;
        return;
    }
    public override void Doing()
    {
        if (Vector3.Distance(npc.transform.position, npc.posOfStation) <= radius)
        {
            npc.SetState(idle);
            return;
        }
        npc.MoveToStand();
    }
}
