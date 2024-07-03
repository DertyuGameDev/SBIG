using UnityEngine;
using UnityEngine.Events;

public class Anim : MonoBehaviour
{
    public GameObject g;
    public void SetupAnim(Animator animator)
    {
        animator.SetBool("Open", !animator.GetBool("Open"));
        if (animator.GetBool("Open"))
        {
            g.SetActive(true);
        }
        else
        {
            g.SetActive(false);
        }
    }
}
