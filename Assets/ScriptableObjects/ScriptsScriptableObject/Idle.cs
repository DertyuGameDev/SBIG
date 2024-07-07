using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "Scriptable Objects/Idle")]
public class Idle : Stating
{
    public float idleTime;
    float time;
    public override void Init()
    {
        idleTime = NPC.delayIdle;
        IsFinished = false;
        return;
    }
    public override void Doing()
    {
        npc.animator.SetFloat("XInput", 0);
        npc.animator.SetFloat("YInput", -1);
        npc.animator.SetFloat("AnimMag", 0);
        time += Time.deltaTime;
        if (time >= idleTime)
        {
            IsFinished = true;
            return;
        }
    }
}
