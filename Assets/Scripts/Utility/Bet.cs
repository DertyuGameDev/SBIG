using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bet : MonoBehaviour
{
    public MicrophoneInput micro;
    [Header("Objects")]
    public Slider progress;
    public Slider betSlider;
    public Slider test;
    public Slider mainMicro;
    public Button chooseBut, rollBut;
    public Image nowBet;
    public GameObject cube, exit;
    public GameObject imageCube;
    public AnimationClip rolling;
    public AnimationClip[] animations;
    public TextMeshProUGUI textBet;
    public TextMeshProUGUI textPlus;
    public List<Sprite> sides = new List<Sprite>();
    [Header("Number Attributes")]
    public int number;
    public float bet;
    public float thresold;
    [Range(0f, 100f)]
    public float indicator;
    bool roll;
    public void Update()
    {
        progress.value = indicator;
        nowBet.sprite = sides[number - 1];
        if (number == 0 || betSlider.value == 0)
        {
            rollBut.interactable = false;
        }
        else
        {
            rollBut.interactable = true;
        }
        betSlider.minValue = 20 * StocksManager.countDays;
        if (PlayerPrefs.GetFloat("MoneyGosha", 0) > 0)
        {
            betSlider.interactable = true;
            betSlider.maxValue = PlayerPrefs.GetFloat("MoneyGosha", 0);
        }
        else
        {
            betSlider.interactable = false;
            betSlider.value = 0;
        }
        textBet.text = string.Format("{0:f2}$", betSlider.value.ToString());
        if (roll)
        {
            if (indicator <= 0)
            {
                indicator = 0;
                micro.slider = mainMicro;
                roll = false;
                exit.transform.DOLocalMoveY(432, 0.1f);
                imageCube.transform.DOScale(1, 1);
                nowBet.transform.DOMoveY(444, 1);
                progress.transform.DOLocalMoveY(-600, 1);
                mainMicro.transform.DOLocalMoveX(-844, 1);
                betSlider.transform.DOLocalMoveY(493, 1);
                chooseBut.transform.DOLocalMoveX(885, 1);
                rollBut.transform.DOLocalMoveY(-366, 1);
                cube.GetComponent<Animator>().Play("Open");
                return;
            }
            if (indicator >= 100)
            {
                roll = false;
                indicator = 0;
                StartCoroutine(Rolling());
                return;
            }
            imageCube.transform.DOScale(new Vector3(2.5f / 100 * (indicator + 1), 2.5f / 100 * (indicator + 1), 2.5f / 100 * (indicator + 1)), 0.1f);
            if (micro.result > thresold)
            {
                indicator += 1.3f * micro.result;
            }
            else
            {
                indicator -= 0.07f;
            }
        }
    }
    public void ChooseBet(int numberBet)
    {
        number = numberBet;
    }
    public void Roll(float threesold)
    {
        imageCube.GetComponent<Animator>().Play(rolling.name);
        bet = betSlider.value;
        thresold = threesold;
        indicator = 20;
        micro.slider = test;
        roll = true;
        exit.transform.DOLocalMoveY(676, 0.1f);
        nowBet.transform.DOMoveY(666, 1);
        progress.transform.DOLocalMoveY(-400, 1);
        mainMicro.transform.DOLocalMoveX(-1123, 1);
        betSlider.transform.DOLocalMoveY(723, 1);
        chooseBut.transform.DOLocalMoveX(1086, 1);
        rollBut.transform.DOLocalMoveY(-666, 1);
        cube.GetComponent<Animator>().Play("Close");
    }
    public IEnumerator Rolling()
    {
        progress.transform.DOLocalMoveY(-600, 1);
        int n = 1;
        for (int i = 0; i < 6; i++)
        {
            n += Random.Range(0, 2);
        }
        imageCube.GetComponent<Animator>().Play(animations[n - 1].name);
        yield return new WaitForSeconds(1);
        if (n == number)
        {
            PlayerPrefs.SetFloat("MoneyGosha", PlayerPrefs.GetFloat("MoneyGosha", 0) + bet * 2);
            textPlus.text = string.Format("+{0:f}$", bet * 2);
            textPlus.color = Color.green;
            textPlus.DOFade(1, 0.1f);
            textPlus.DOFade(0, 2);
        }
        else
        {
            PlayerPrefs.SetFloat("MoneyGosha", PlayerPrefs.GetFloat("MoneyGosha", 0) - bet);
            textPlus.text = string.Format("-{0:f}$", bet);
            textPlus.color = Color.red;
            textPlus.DOFade(1, 0.1f);
            textPlus.DOFade(0, 2);
        }
        indicator = 0;
        micro.slider = mainMicro;
        roll = false;
        nowBet.transform.DOMoveY(444, 1);
        mainMicro.transform.DOLocalMoveX(-844, 1);
        betSlider.transform.DOLocalMoveY(493, 1);
        chooseBut.transform.DOLocalMoveX(885, 1);
        rollBut.transform.DOLocalMoveY(-366, 1);
        cube.GetComponent<Animator>().Play("Open");

    }
}
