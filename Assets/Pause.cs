using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public StocksManager st;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Convert.ToInt32(pause.activeSelf);
            st.AudioEn(pause.activeSelf);
            pause.SetActive(!pause.activeSelf);
        }
    }
}
