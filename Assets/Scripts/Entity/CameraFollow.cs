using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Camera")]
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;
    [Header("Shake Camera")]
    public Vignette vignete;
    public Volume volume;
    public MicrophoneInput input;
    public Transform camera;
    public bool shake;
    public float strenght;
    public float vibro;
    public float lerp;
    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(Mathf.Clamp(target.position.x, -2.40f, 18f), Mathf.Clamp(target.position.y + yOffset, -7.95f, 1.10f), -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

        if (shake)
        {
            float x = Random.Range(-vibro * strenght, vibro * strenght);
            float y = Random.Range(-vibro * strenght, vibro * strenght);
            vibro = input.result;
            volume.profile.TryGet(out vignete);
            vignete.intensity.value = 0.3f * vibro;
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, new Vector3(x, y, 0), lerp * 0.01f);
        }
        else
        {
            camera.transform.localPosition = Vector3.zero;
        }
    }
}
