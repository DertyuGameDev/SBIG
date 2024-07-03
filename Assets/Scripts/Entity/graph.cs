using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class graph : MonoBehaviour
{
    public Camera cam;
    public float scale;

    public GameObject XDivision;
    public float AmounXCivivsons;
    public GameObject YDivision;
    public float AmounYCivisions;

    public GameObject point;
    public GameObject[] Points;

    public Vector3[] Tops;
    public float strenghtlerp, strenghtWheel;
    public int clampOrt = 20;
    public int ind;
    public string GetDay()
    {
        return "Day: " + ind + ": " + (Points[ind].transform.position.y * 1000).ToString() + "$ ";
    }
    private void Update()
    {
        if (Points.Length != 0)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Points[ind].transform.position + new Vector3(0, 0, -3), strenghtlerp * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ind += 1;
            ind %= Points.Length;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ind -= 1;
            if (ind < 0)
            {
                ind = Points.Length - 1;
            }
            else
            {
                ind %= Points.Length;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * strenghtWheel;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 5, clampOrt);
        }
    }
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
        }
    }

    public void DrowGraph(Vector3[] Arr)
    {
        Points = new GameObject[Arr.Length];
        for (int i = 0; i < Points.Length; i++)
            Points[i] = Instantiate(point, Vector3.zero, transform.rotation);
        Tops = new Vector3[Arr.Length];
        for (int j = 0; j < Arr.Length; j++)
        {
            Tops[j] = new Vector3(Arr[j].x * scale, Arr[j].y * scale, 0);
        }
        for (int k = 0; k < Arr.Length; k++)
        {
            Points[k].transform.position = Tops[k];
        }
        for (int k = 0; k < Arr.Length; k++)
        {
            if (k != Arr.Length - 1)
            {
                Points[k].GetComponent<Point>().SetupLine(Points[k].transform.position, Points[k + 1].transform.position);
            }
        }   
    }
}
