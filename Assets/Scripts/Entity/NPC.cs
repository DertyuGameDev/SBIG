using DG.Tweening;
using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [HideInInspector] public float time;
    public static float delayIdle = 2;
    public float dist;
    public Vector3 pos;
    public Animator animator;
    
    public Stating startState;
    public Stating idleState;
    public Stating walkState;
    public Stating buyState;
    public NavMeshAgent nav;

    public float lifeTime;
    [HideInInspector] public bool already;
    public float speed = 2, speedRot = 2;
    public Stating currentState;
    [HideInInspector] public Transform posOfStation;

    [Header("Debug")]
    public float delay;
    public float money = 0;
    [HideInInspector] public string nameTr;
    void Start()
    {
        animator.SetBool(string.Format("{0}", Random.Range(1, 4)), true);
        money = StocksManager.moneyStart;
        nav.updateRotation = false;
        nav.updateUpAxis = false;
        SetState(startState);
    }
    public void Dest()
    {
        already = true;
    }
    public IEnumerator dest()
    {
        yield return new WaitForSeconds(1);
        SpawnerNPC.drop();
        Destroy(gameObject);
    }
    void Update()
    {
        time += Time.deltaTime;
        if (already)
        {
            GetComponent<SpriteRenderer>().DOFade(0, 1);
            StartCoroutine(dest());
            already = false;
        }
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
