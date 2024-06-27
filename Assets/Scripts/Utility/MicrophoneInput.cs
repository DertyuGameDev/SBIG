using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneInput : MonoBehaviour
{
    int sampleWindow = 64;
    public string nameMicro;
    [Header("Attribute Settings")]
    public float maxSenc;
    public float minSens;
    public float sensitivityMicrophone;
    public float thresold;
    public static Bonus bonus = null;

    AudioClip clip;

    [Header("UI")]
    public Slider slider;
    public Slider sensSlider;
    public TMP_Dropdown drop;

    public void MicrophoneChoose()
    {
        nameMicro = Microphone.devices[drop.value];
        clip = Microphone.Start(nameMicro, true, 20, AudioSettings.outputSampleRate);
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
    private void Start()
    {
        List<TMP_Dropdown.OptionData> listOptions = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            TMP_Dropdown.OptionData g = new TMP_Dropdown.OptionData();
            g.text = Microphone.devices[i];
            listOptions.Add(g);
        }
        drop.options = listOptions;
        sensSlider.minValue = minSens; 
        sensSlider.maxValue = maxSenc; 
        sensSlider.value = PlayerPrefs.GetFloat("Sensitivity", 1);
        MicrophoneChoose();
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensSlider.value);
        sensitivityMicrophone = sensSlider.value;
        float loud = GetLouder(Microphone.GetPosition(nameMicro), clip);

        if (loud < thresold) { loud = 0; }
        print(loud);
        if (bonus)
        {
            slider.value = loud * PlayerPrefs.GetFloat("Sensitivity") + bonus.bonusToVoice;
        }
        else
        {
            slider.value = loud * PlayerPrefs.GetFloat("Sensitivity");
        }
    }
}
