using UnityEngine;

public class TradersNPC : MonoBehaviour
{
    int sampleWindow = 64;
    public AudioSource source;
    public float result;
    [Header("Attribute Settings")]
    public float thresold;
    public Traders me;
    public Bonus bonus = null;

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
    private void Update()
    {
        float loud = GetLouder(source.timeSamples, source.clip);

        if (loud < thresold) { loud = 0.1f; }
        loud = Mathf.Clamp(loud, 0.1f, 3);
        if (bonus)
        {
            result = loud * 2f + bonus.bonusToVoice;
        }
        else
        {
            result = loud * 2f;
        }
        me.voiceStrenght = result;
    }
}
