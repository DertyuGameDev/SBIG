using System;
using UnityEngine;
using UnityEngine.UI;

public class Ultra : MonoBehaviour
{
    public static Action stop, start;
    public float res;
    public Bonus b;
    public MicrophoneInput mic;
    public CameraFollow cam;
    public float strenght;
    public float vibro;
    public float lerp;
    public Slider me;
    public int state = 0;
    [Range(0, 100)]
    public int maxSpeed;
    public float speedDown;
    public float speed;
    public float minValues;
    public float threesold;
    public Bonus bonus, myBonus;
    public static bool IsUltaNow = false;
    public void Flip()
    {
        me.value = 0;
        MicrophoneInput.bonus = bonus;
        bonus = null;
        cam.StopShake();
    }
    public void St()
    {
        me.value = 0;
        cam.StartShake();
    }
    private void Awake()
    {
        stop += Flip;
        start += St;
    }
    void Update()
    {
        res = mic.result;
        b = MicrophoneInput.bonus;
        me.maxValue = maxSpeed;
        switch (state)
        {
            case 0:
                cam.shake = true;
                if (mic.result > threesold)
                {
                    me.value += mic.result * speed;
                }
                else
                {
                    me.value -= minValues;
                }
                if (me.value >= maxSpeed)
                {
                    state = 1;
                }
                break;
            case 1:
                bonus = MicrophoneInput.bonus;
                MicrophoneInput.bonus = myBonus;
                state = 2;
                break;
            case 2:
                me.value -= speedDown;
                cam.Shake(vibro, lerp, strenght);
                if (me.value == 0)
                {
                    state = 3;
                }
                break;
            case 3:
                MicrophoneInput.bonus = bonus;
                bonus = null;
                state = 0;
                break;
        }
        if (state == 2)
        {
            IsUltaNow = true;
        }
        else
        {
            IsUltaNow = false;
        }
    }
}
