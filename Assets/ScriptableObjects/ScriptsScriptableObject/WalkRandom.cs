using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkRandom", menuName = "Scriptable Objects/WalkRandom")]
public class WalkRandom : Stating
{
    public float radius;
    public Stating idle;
    public bool can;
    public override void Init()
    {
        IsFinished = false;
        return;
    }
    public override void Doing()
    {
        npc.animator.SetFloat("XInput", Mathf.Clamp(npc.nav.velocity.x, -1, 1));
        npc.animator.SetFloat("YInput", Mathf.Clamp(npc.nav.velocity.y, -1, 1));
        npc.animator.SetFloat("AnimMag", npc.nav.velocity.y * npc.nav.velocity.y + npc.nav.velocity.x * npc.nav.velocity.x);
        if (Vector3.Distance(npc.transform.position, new Vector3(1, 0, 0)) <= radius)
        {
            npc.nav.isStopped = true;
            npc.SetState(idle);
            return;
        }
        if (npc.time > npc.lifeTime && !npc.already && can)
        {
            can = false;
            npc.already = true;
        }
        npc.nav.isStopped = false;
        npc.MoveTo(new Vector3(1, 0, 0));
    }
}
