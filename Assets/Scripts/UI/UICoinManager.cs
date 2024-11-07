using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UICoinManager : MonoBehaviour
{
    public static UICoinManager instance;
    private int coinCount;
    [SerializeField] private TextMeshProUGUI coinCounterText;
    [SerializeField] private GameObject miniCoinPrefab; // Küçük coin prefab'ı
    [SerializeField]private Transform uiTarget; // UI hedefi (CoinCounterText’in konumu)

    void Awake() 
    {
        instance = this; 
        coinCount = 0;  
    }

    public void CollectCoin()
    {
        coinCount++;
        coinCounterText.text = ": " + coinCount.ToString();

        // Coroutine başlat, her 0.2 saniyede bir miniCoin oluştur
        StartCoroutine(SpawnMiniCoins());
    }

    private IEnumerator SpawnMiniCoins()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject miniCoin = Instantiate(miniCoinPrefab, transform.position, Quaternion.identity);
            AnimateMiniCoinToUI(miniCoin);
            yield return new WaitForSeconds(0.1f); // Her 0.2 saniyede bir miniCoin oluştur
        }
    }

    private void AnimateMiniCoinToUI(GameObject miniCoin)
    {
        // Küçük coin'i UI'ye taşıyan animasyon
        miniCoin.transform.localPosition = uiTarget.parent.position; // UI'nin merkezinde başlat

        miniCoin.transform.DOMove(uiTarget.position, 1f).OnComplete(() =>
        {
            Destroy(miniCoin); // UI'ye ulaşınca yok et
        });

        // x ekseninde sürekli dönme animasyonu
        miniCoin.transform.DORotate(new Vector3(360, 0, 0), 0.5f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

}
