using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AnimButtonScript : MonoBehaviour
{
    public Animator anim;
    public Light2D light;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void OnMouseOver()
    {
        StopAllCoroutines();
        anim.SetBool("Over", true);
        light.intensity = Mathf.Lerp(light.intensity, 0.84f, 5 * Time.deltaTime);
    }
    public void OnMouseExit()
    {
        anim.SetBool("Over", false);
        StartCoroutine(fade());
    }
    public IEnumerator fade()
    {
        light.intensity = Mathf.Lerp(light.intensity, 0, 5 * Time.deltaTime);
        yield return new WaitForSeconds(0.001f);
        StartCoroutine(fade());
    }
}
