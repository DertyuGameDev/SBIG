using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public PlayerController playerController;
    public StocksManager st;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerController.enabled)
        {
            Time.timeScale = Convert.ToInt32(pause.activeSelf);
            pause.SetActive(!pause.activeSelf);
        }
    }
}
