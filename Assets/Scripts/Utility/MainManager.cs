using UnityEngine;

public class MainManager : MonoBehaviour
{
    public graph graph_;
    Vector3[] RandArr;

    void Start()
    {
        RandArr = new Vector3[Random.Range(3, 10)];
        for (int i = 0; i / 2 < RandArr.Length; i = i + 2)
            RandArr[i / 2] = new Vector3(Random.Range(i, i + 2), Random.Range(0, 10), 0);

        graph_.xOy();
        graph_.DrowGraph(RandArr);
        graph_.LocateGraph(RandArr);
    }
}
