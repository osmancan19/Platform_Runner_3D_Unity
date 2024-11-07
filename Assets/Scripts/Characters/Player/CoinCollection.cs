using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Coin"))
        {
            UICoinManager.instance.CollectCoin();
            SoundManager.instance.PlayCollectCoinSound();
            Destroy(other.gameObject);
        }
    } 
}
