using DG.Tweening;
using UnityEngine;

public class DotWeenScript : MonoBehaviour
{
    public void ScaleBig()
    {
        gameObject.transform.DOScale(0.6196816f, 0.3f);
    }
    public void ScaleSmall()
    {
        gameObject.transform.DOScale(1, 0.7f);
    }
}
