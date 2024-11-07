using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UICountDownManager : MonoBehaviour
{
    private int countDownStartNumber;
    public TextMeshProUGUI countDownText;
    private int countDownCount;
    private string countDownEndMessage;
    [SerializeField]private CanvasGroup canvasGroup;
    private float fadeDuration;
    private bool isStarted;

    void Start()
    {
        isStarted = false;
        fadeDuration = 0.3f;
        countDownStartNumber = 3;
        countDownEndMessage = "GO!";
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isStarted)
        {            
            isStarted = true;
            StartCountDown();
        }
        
    }
    private void StartCountDown()
    {
        countDownCount = countDownStartNumber;
        SoundManager.instance.PlayCountDownSound(); 
        UIManager.Instance.FingerTapped();  
        StartCoroutine(CountDownCoroutine());
    }
    private IEnumerator CountDownCoroutine()
    {
        if(countDownCount>0)
        {
            countDownText.text = countDownCount.ToString();
        }
        else
        {
            countDownText.text = countDownEndMessage;
        }
        
        canvasGroup.DOFade(1f,fadeDuration).SetUpdate(true).OnComplete(()=>
        {
            canvasGroup.DOFade(0f,fadeDuration).SetUpdate(true).SetDelay(fadeDuration);
        });

        countDownText.rectTransform.DOScale(Vector3.one,fadeDuration).SetUpdate(true).OnComplete(()=>
        {
             countDownText.rectTransform.DOScale(Vector3.one,fadeDuration).SetUpdate(true).SetDelay(fadeDuration);
        });

        yield return new WaitForSeconds(1f);

        countDownCount--;

        if(countDownCount >= 0)
        {
            StartCoroutine(CountDownCoroutine());
        }
        else
        {
            canvasGroup.alpha = 1f;
            UIManager.Instance.ShowInGameUI();
            CharacterPool.instance.EnableMovement();
            SoundManager.instance.PlayBackgroundSound();
        }
    }

}
