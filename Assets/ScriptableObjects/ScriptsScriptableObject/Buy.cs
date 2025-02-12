using UnityEngine;

[CreateAssetMenu(fileName = "Buy", menuName = "Scriptable Objects/Buy")]
public class Buy : Stating
{
    [Range(1, 100)]
    public float chance;
    public float timeMind;
    public float time;
    public float radius;
    public GameObject dollar;
    public Stating walkState;
    public override void Init()
    {
        return;
    }
    public override void Doing()
    {
        npc.animator.SetFloat("XInput", Mathf.Clamp(npc.posOfStation.position.x - npc.transform.position.x, -1, 1));
        npc.animator.SetFloat("YInput", Mathf.Clamp(npc.posOfStation.position.y - npc.transform.position.y, -1, 1));
        npc.animator.SetFloat("AnimMag", 0);
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
            Instantiate(dollar, new Vector3(npc.posOfStation.position.x + (npc.posOfStation.position.x / 2 - npc.transform.position.x / 2),
                npc.posOfStation.position.y + (npc.posOfStation.position.y / 2 - npc.transform.position.y / 2), 0), Quaternion.identity);
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
