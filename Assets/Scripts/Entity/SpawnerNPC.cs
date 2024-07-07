using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNPC : MonoBehaviour
{
    public static Action drop;
    public GameObject prefabNPC;
    public float delay;
    public GameObject[] pountsSpawner;
    public Animator animator;
    public int limit;
    public int a;
    private void OnDisable()
    {
        a = 0;
    }
    private void Start()
    {
        drop += Drop;
        StopAllCoroutines();
        StartCoroutine(DelaySpawn());
    }
    public void Drop()
    {
        a -= 1;
    }
    public void Spawn()
    {
        for (int i = 0;i < pountsSpawner.Length;i++)
        {
            if (UnityEngine.Random.Range(0, 2) == 1 && a < limit)
            {
                a += 1;
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
