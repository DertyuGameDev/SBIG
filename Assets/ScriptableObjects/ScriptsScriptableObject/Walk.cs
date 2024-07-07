using UnityEngine;

[CreateAssetMenu(fileName = "Walk", menuName = "Scriptable Objects/Walk")]
public class Walk : Stating
{
    public float radius;
    [HideInInspector] public float time;
    public float maxTime;
    public Stating idle, buy;
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
        if (Vector2.Distance(npc.transform.position, npc.posOfStation.position) <= radius) 
        {
            npc.nav.isStopped = true;
            npc.SetState(buy);
            return;
        }
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            npc.nav.isStopped = true;
            npc.SetState(idle);
            return;
        }
        npc.nav.isStopped = false;
        npc.MoveToStand();
    }
}
