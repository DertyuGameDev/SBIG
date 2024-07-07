using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Anim : MonoBehaviour
{
    public bool open;
    public void SetupAnim(Transform p)
    {
        if (open)
        {
            p.transform.DOLocalMoveY(-1165, 0.4f);
            open = false;
        }
        else
        {
            p.transform.DOLocalMoveY(0, 0.4f);
            open = true;
        }
    }
}
