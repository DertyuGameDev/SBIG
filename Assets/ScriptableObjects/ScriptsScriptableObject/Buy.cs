using UnityEngine;

[CreateAssetMenu(fileName = "Buy", menuName = "Scriptable Objects/Buy")]
public class Buy : Stating
{
    [Range(1, 100)]
    public float chance;
    public float timeMind;
    public float time;
    public float radius;
    public Stating walkState;
    public override void Init()
    {
        return;
    }
    public override void Doing()
    {
        if (Vector3.Distance(npc.transform.position, npc.posOfStation.position) > radius)
        {
            npc.nav.isStopped = true;
            npc.SetState(npc.walkState);
            return;
        }
        time += Time.deltaTime;
        if (time < timeMind)
        {
            return;
        }
        float h = Random.Range(0, 100);
        if (h <= chance)
        {
            npc.nav.isStopped = true;
            Traders tr = StocksManager.dTr[npc.nameTr];
            tr.countBuyToday += 1;
            npc.money -= tr.costOneStock;
            npc.posOfStation = null;
            npc.SetState(walkState);
        }
        else
        {
            npc.nav.isStopped = true;
            npc.posOfStation = null;
            npc.SetState(walkState);
        }
    }
}
