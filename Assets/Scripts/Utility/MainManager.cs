using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public Traders def;
    public Traders me;
    public graph graph_;
    public Vector3[] RandArr;
    GameObject[] points;
    public TextMeshProUGUI nameText, dayCostText;
    private void Start()
    {
        me = def;
        MakeGraph();
    }
    private void Update()
    {
        dayCostText.text = graph_.GetDay();
        nameText.text = me.nameTrader + "`s cost of all stocks:";
        if (GameObject.FindGameObjectsWithTag("Point").Length < me.stocksInfo.Count + graph_.AmounXCivivsons + graph_.AmounYCivisions)
            MakeGraph();
    }
    void MakeGraph()
    {
        foreach(var r in GameObject.FindGameObjectsWithTag("Point")) 
        { 
            Destroy(r);
        }
        RandArr = new Vector3[me.stocksInfo.Count];
        for (int i = 0; i  < me.stocksInfo.Count; i++)
            RandArr[i] = new Vector3(i, me.stocksInfo[i] / 1000, 0);

        graph_.xOy();
        graph_.DrowGraph(RandArr);
    }

    public void SetTrader(Traders traders)
    {
        me = traders;
        MakeGraph();
    }
}
