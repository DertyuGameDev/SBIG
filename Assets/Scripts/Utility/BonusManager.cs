using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public GameObject content;
    public Bonus[] bonusList;
    public GameObject prefabBonus;
    private void Awake()
    {
        foreach(var item in bonusList)
        {
            GameObject gameObject = Instantiate(prefabBonus, content.transform);
            gameObject.GetComponent<Card>().bonus = item;
            gameObject.GetComponent<Card>().sprite.sprite = item.sprite;
            gameObject.GetComponent<Card>().nameText.text = item.Name;
            gameObject.GetComponent<Card>().costText.text = item.cost.ToString() + "$";
        }
    }
}
