using System.Drawing;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public static float delayIdle = 2;

    public Stating startState;
    public Stating idleState;
    public Stating walkState;
    public Stating buyState;

    public float speed = 2, speedRot = 2;
    public Stating currentState;
    [HideInInspector] public Vector3 posOfStation;

    [Header("Debug")]
    public float delay;
    public float money = 0;
    [HideInInspector] public string nameTr;
    void Start()
    {
        SetState(startState);
    }
    void Update()
    {
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
        transform.position = Vector3.Lerp(transform.position, posOfStation, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                Mathf.Atan2(posOfStation.y - transform.position.y, posOfStation.x - transform.position.x) * Mathf.Rad2Deg - 90), Time.deltaTime * speedRot);
    }
}
