using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class DebugScript : MonoBehaviour
{
    public List<Traders> traders;
    public TextMeshProUGUI[] t;
    void Update()
    {
        for(int i = 0; i < t.Length; i++)
        {
            Dictionary<string, Dictionary<float, Traders>> d = traders[i].precentsOfOther;
            string n = "";
            foreach(var item in d.Keys) 
            { 
                foreach(var y in d[item].Keys)
                {
                    n += item + " : " + y + ";\n";
                }
            }
            n += PlayerPrefs.GetFloat("Money" + traders[i].nameTrader, 0) + "\n";
            t[i].text = n;
        }
    }
}
