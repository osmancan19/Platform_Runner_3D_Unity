using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject UIDuringGame; // Oyun sırasındaki panel
    public GameObject UIEndGame; // Duvar boyama UI paneli
    public GameObject UICountDown; // Duvar boyama UI paneli
    public GameObject UIFingerTap;
    public GameObject UIFingerSlide;
    public GameObject UIGameOver;
    public CanvasGroup canvasGroup;    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void FingerTapped()
    {       
        canvasGroup.alpha = 0f;
        Camera.main.GetComponent<PostProcessVolume>().isGlobal = false;
        UICountDown.SetActive(true);
        UIFingerTap.SetActive(false);
    }
    public void ShowInGameUI()
    {        
        UICountDown.SetActive(false);
        UIDuringGame.SetActive(true);
    }
    public void ShowPaintingUI()
    {
        // Duvar boyama UI'sini aktif hale getir
        UIEndGame.SetActive(true);
        UIFingerSlide.SetActive(true);
        // Oyun sırasındaki panel kapat
        UIDuringGame.SetActive(false);
    }
    public void ShowGameOverUI()
    {
        UIGameOver.SetActive(true);
        UIEndGame.SetActive(false);
    }
    public void FingerSlided()
    {
        UIFingerSlide.SetActive(false);
    }
    public bool IsFingerSlideActive()
    {
        return UIFingerSlide.activeSelf;
    }

}
