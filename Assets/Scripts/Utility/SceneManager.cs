using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void StartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
