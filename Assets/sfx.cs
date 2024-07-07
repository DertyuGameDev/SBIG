using UnityEngine;

public class sfx : MonoBehaviour
{
    public AudioSource SFX;
    public void Awake()
    {
        SFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }
    public void PlayOn(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}
