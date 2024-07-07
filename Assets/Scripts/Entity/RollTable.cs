using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RollTable : MonoBehaviour
{
    public Transform rollCanvas;
    public PlayerController playerController;
    public GameObject indicator;
    public bool isOpen = false;
    bool en;
    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && !isOpen)
        {
            en = true;
        }
        else
        {
            en = false;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        Close(rollCanvas);
    }
    private void Update()
    {
        indicator.SetActive(en);
        if (en && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            rollCanvas.transform.DOLocalMoveX(0, 0.4f);
            isOpen = true;
        }
    }
    public void Close(Transform tr)
    {
        isOpen = false;
        en = true;
        tr.DOLocalMoveX(2000, 0.4f);
    }
}
