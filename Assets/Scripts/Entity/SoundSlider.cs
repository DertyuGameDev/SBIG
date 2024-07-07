using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sound", 0);
    }
}
