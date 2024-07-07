using UnityEngine;

public class Continue : MonoBehaviour
{
    public Anim[] anim;
    public void ContinueGame()
    {
        foreach(Anim a in anim)
        {
            if (a.open)
            {
                return;
            }
        }
        StocksManager.cont();
    }
}
