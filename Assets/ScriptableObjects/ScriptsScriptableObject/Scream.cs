using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "Scream", menuName = "Scriptable Objects/Scream")]
public class Scream : StatingTraders
{
    public int sampleWindow = 64;
    public float thresold;
    public override void Init()
    {

    }
    public override void Doing()
    {
        float loud = GetLouder(trader.source.timeSamples, trader.source.clip);

        if (loud < thresold) { loud = 0.1f; }
        loud = Mathf.Clamp(loud, 0.1f, 3);
        if (trader.bonus)
        {
            trader.result = loud * 2f + trader.bonus.bonusToVoice;
        }
        else
        {
            trader.result = loud * 2f;
        }
        trader.me.voiceStrenght = trader.result;
        if (Random.Range(0, 2) == 1)
        {
            trader.me.voiceStrenght += Random.Range(0.1f, 0.5f);
        }
        IsFinished = true;
    }

    public float GetLouder(int clipPos, AudioClip clip)
    {
        int startPos = clipPos - sampleWindow;

        if (startPos < 0) { return 0; }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPos);

        float totalLoudness = 0;

        foreach (var wave in waveData)
        {
            totalLoudness += Mathf.Abs(wave);
        }

        return totalLoudness / sampleWindow;
    }
}
