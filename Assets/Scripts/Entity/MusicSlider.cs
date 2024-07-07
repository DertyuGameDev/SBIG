using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music", 0);
    }
}
