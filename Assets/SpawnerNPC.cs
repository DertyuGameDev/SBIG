using System.Collections;
using UnityEngine;

public class SpawnerNPC : MonoBehaviour
{
    public GameObject prefabNPC;
    public float delay;
    public void OnEnable()
    {
        StartCoroutine(spawnNps());
    }
    public void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator spawnNps()
    {
        Instantiate(prefabNPC, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        StartCoroutine(spawnNps());
    }
}
