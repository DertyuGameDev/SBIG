using UnityEngine;

public class Choose : MonoBehaviour
{
    bool open;
    public Animator anim;
    public void Open()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Open") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            if (open)
            {
                anim.Play("Close");
            }
            else
            {
                anim.Play("Open");
            }
            open = !open;
        }
    }
}
