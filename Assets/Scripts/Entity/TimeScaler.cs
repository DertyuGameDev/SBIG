using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("First", 0) == 0)
        {
            PlayerPrefs.SetInt("First", 1);
            Time.timeScale = 0;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void StartTime()
    {
        Time.timeScale = 1;
    }
}
