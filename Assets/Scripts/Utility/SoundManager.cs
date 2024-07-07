using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] sourcesMusic, sourcesSound;
    public void MusicChange(Slider slider)
    {
        PlayerPrefs.SetFloat("Music", slider.value);
    }
    public void SoundChange(Slider slider)
    {
        PlayerPrefs.SetFloat("Sound", slider.value);
    }
    private void Update()
    {
        foreach (var source in sourcesMusic)
        {
            source.volume = PlayerPrefs.GetFloat("Music", 0);
        }
        foreach (var source in sourcesSound)
        {
            source.volume = PlayerPrefs.GetFloat("Sound", 0);
        }
    }
}
