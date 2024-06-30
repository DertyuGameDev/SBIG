using UnityEngine;

public class graph : MonoBehaviour
{

    public float scale;

    public GameObject XDivision;
    public float AmounXCivivsons;
    public GameObject YDivision;
    public float AmounYCivisions;

    public GameObject point;
    GameObject[] Points;

    Vector3[] Tops;

    LineRenderer lineRenderer;

    public void xOy()
    {
        for (int i = 0; i < AmounXCivivsons; i++)
        {
            GameObject division;
            division = Instantiate(XDivision, new Vector3(i * scale, 0, 0), transform.rotation);

            division.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = i.ToString();
        }
        for (int i = 0; i < AmounYCivisions; i++)
        {
            GameObject division;
            division = Instantiate(YDivision, new Vector3(0, i * scale, 0), transform.rotation);

            division.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = i.ToString();
        }
    }

    public void DrowGraph(Vector3[] Arr)
    {
        Tops = Arr;
        Points = new GameObject[Tops.Length];

        lineRenderer = GetComponent<LineRenderer>();

        for (int i = 0; i < Points.Length; i++)
            Points[i] = Instantiate(point, Vector3.zero, transform.rotation);
            
    }
    public void LocateGraph(Vector3[] Arr)
    {
        for (int i = 0; i < Arr.Length; i++)
        {
            Tops[i] = new Vector3(Arr[i].x * scale, Arr[i].y * scale, 0);
        }
        for (int i = 0; i < Arr.Length; i++)
        {
            Points[i].transform.position = Tops[i];
        }

        lineRenderer.positionCount = Tops.Length;
        lineRenderer.SetPositions(Tops);
    }
}
