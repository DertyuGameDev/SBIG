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
        if (Vector3.Distance(npc.transform.position, new Vector3(1, 1, 0)) <= radius)
        {
            npc.nav.isStopped = true;
            npc.SetState(idle);
            return;
        }
        npc.nav.isStopped = false;
        npc.MoveTo(new Vector3(1, 1, 0));
    }
}
