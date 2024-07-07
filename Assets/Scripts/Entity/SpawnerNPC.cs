using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNPC : MonoBehaviour
{
    public static Action stop, start, drop;
    public GameObject prefabNPC;
    public float delay;
    public GameObject[] pountsSpawner;
    public Animator animator;
    public int a;
    private void Start()
    {
        drop += Drop;
        print(BestBuyManager.getPrecent("Gosha"));
        stop += Stop;
        start += Start;
        StartCoroutine(DelaySpawn());
    }
    public void Drop()
    {
        a -= 1;
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
    public void StartSpawn()
    {
        StopAllCoroutines();
        StartCoroutine(DelaySpawn());
    }
    public void Spawn()
    {
        for (int i = 0;i < pountsSpawner.Length;i++)
        {
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                Instantiate(prefabNPC, pountsSpawner[i].transform.position, Quaternion.identity);
            }
        }
    }
    public IEnumerator DelaySpawn()
    {
        animator.Play("Elevator");
        yield return new WaitForSeconds(delay);
        StartCoroutine(DelaySpawn());
    }
}
