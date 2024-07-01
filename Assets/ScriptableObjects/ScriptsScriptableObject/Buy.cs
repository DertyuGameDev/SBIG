using UnityEngine;

[CreateAssetMenu(fileName = "Buy", menuName = "Scriptable Objects/Buy")]
public class Buy : Stating
{
    [Range(1, 100)]
    public float chance;
    public float timeMind;
    public float time;
    public Stating walkState;
    public override void Init()
    {
        return;
    }
    public override void Doing()
    {
        time += Time.deltaTime;
        if (time < timeMind)
        {
            return;
        }
        float h = Random.Range(0, 100);
        if (h <= chance)
        {
            Traders tr = StocksManager.dTr[npc.nameTr];
            tr.countBuyToday += 1;
            npc.money -= tr.costOneStock;
            npc.posOfStation = Vector3.zero;
            npc.SetState(walkState);
        }
        else
        {
            npc.posOfStation = Vector3.zero;
            npc.SetState(walkState);
        }
    }
}
