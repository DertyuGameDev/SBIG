using UnityEngine;

public class TradeZone : MonoBehaviour
{
    public Animator anim;
    public MicrophoneInput mic;
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        anim.SetBool("Trade", true);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("Trade", false);
        mic.enabled = false;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (anim.GetFloat("AnimMag") < 0.1f)
        {
            mic.enabled = true;
        }
        else
        {
            mic.enabled = false;
        }
    }
}
