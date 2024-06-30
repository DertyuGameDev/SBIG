using UnityEngine;

public class BarChartGenerator : MonoBehaviour
{
    public GameObject barPrefab;
    public Vector3 startPosition;
    public float barSpacing = 1.0f;
    public float[] data;

    void Start()
    {
        GenerateBarChart();
    }

    void GenerateBarChart()
    {
        for (int i = 0; i < data.Length; i++)
        {
            GameObject bar = Instantiate(barPrefab, startPosition + new Vector3(i * barSpacing, 0, 0), Quaternion.identity);
            bar.transform.localScale = new Vector3(1, data[i], 1); 
        }
    }
}

