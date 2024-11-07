using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharacterRankManager : MonoBehaviour
{
    public static UICharacterRankManager instance;
    public TextMeshProUGUI countDownText;

    private bool playerHasFinished = false; // Player'ın finish line'ı geçip geçmediğini kontrol eder
    private int playerFinalRank = 1; // Player'ın finish sonrası sabit rank değeri

    void Awake() 
    {
        instance = this;
    }

    void Update()
    {
        if(CharacterPool.instance.characterMovements.Count > 0)
        {
            CalculatePlayerRank();
        }
    }

    private void CalculatePlayerRank()
    {
        List<CharacterMovement> sortedCharacters = new List<CharacterMovement>(CharacterPool.instance.characterMovements);

        // Karakterleri `z` eksenine göre sıralıyoruz, ancak `hasFinished` durumunu göz önünde bulunduruyoruz
        sortedCharacters.Sort((a, b) =>
        {
            // Eğer `a` bitiş çizgisini geçtiyse ve `b` geçmediyse, `a` önde kalır
            if (a.GetHasFinished() && !b.GetHasFinished()) return -1;
            if (!a.GetHasFinished() && b.GetHasFinished()) return 1;

            // Her iki karakter de bitiş çizgisini geçmediyse `z` pozisyonuna göre sırala
            return b.transform.position.z.CompareTo(a.transform.position.z);
        });

        // Player finish line'ı geçmişse sıralamayı değiştirme
        if (playerHasFinished)
        {
            countDownText.text = "Rank: " + playerFinalRank.ToString();
            return;
        }

        // Player'ın sıralamadaki konumunu bul
        for (int i = 0; i < sortedCharacters.Count; i++)
        {
            if (sortedCharacters[i].GetCharacterType() == CharacterType.Player)
            {
                playerFinalRank = i + 1; // Player'ın mevcut sırasını kaydediyoruz
                break;
            }
        }
        
        countDownText.text = "Rank: " + playerFinalRank.ToString();
    }

    public void SetPlayerHasFinished()
    {
        playerHasFinished = true; // Player'ın finish line'ı geçtiğini belirtiyoruz
    }
}
