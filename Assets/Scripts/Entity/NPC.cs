using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public static float delayIdle = 2;
    public float dist;
    public Vector3 pos;

    public Stating startState;
    public Stating idleState;
    public Stating walkState;
    public Stating buyState;
    public NavMeshAgent nav;

    public float speed = 2, speedRot = 2;
    public Stating currentState;
    [HideInInspector] public Transform posOfStation;

    [Header("Debug")]
    public float delay;
    public float money = 0;
    [HideInInspector] public string nameTr;
    void Start()
    {
        money = StocksManager.moneyStart;
        nav.updateRotation = false;
        nav.updateUpAxis = false;
        SetState(startState);
    }
    void Update()
    {
        if (nameTr != "")
        {
            posOfStation = StocksManager.positions[nameTr];
            pos = posOfStation.position;
        }
        delay = delayIdle;
        money += Time.deltaTime;
        delayIdle -= 0.0001f;
        delayIdle = Mathf.Clamp(delayIdle, 0.5f, 2);
        if (currentState.IsFinished == false)
        {
            currentState.Doing();
        }
        else
        {
            nameTr = StocksManager.IsThereAnyMoney(money);
            if (nameTr != "")
            {
                posOfStation = StocksManager.positions[nameTr];
                SetState(walkState);
            }
            else
            {
                SetState(idleState);
            }
        }
    }
    public void SetState(Stating state)
    {
        currentState = Instantiate(state);
        currentState.npc = this;
        currentState.Init();
    }
    public void MoveToStand()
    {
        dist = Vector3.Distance(transform.position, posOfStation.position);
        nav.SetDestination(posOfStation.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
        //        Mathf.Atan2(posOfStation.position.y - transform.position.y, posOfStation.position.x - transform.position.x) * Mathf.Rad2Deg - 90), Time.deltaTime * speedRot);
    }
    public void MoveTo(Vector3 vec3)
    {
        nav.SetDestination(vec3);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
        //        Mathf.Atan2(vec3.y - transform.position.y, vec3.x - transform.position.x) * Mathf.Rad2Deg - 90), Time.deltaTime * speedRot);
    }
}
