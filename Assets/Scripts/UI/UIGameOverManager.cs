using UnityEngine;
using UnityEngine.UI;

public class UIGameOverManager : MonoBehaviour
{
    public Button closeGameButton; // CloseGameButton'un referansı

    void Start()
    {
        // Butona tıklama olayını dinleyici olarak ekle
        closeGameButton.onClick.AddListener(CloseGame);
    }

    private void CloseGame()
    {
        // Oyun kapanır
        Application.Quit();
        
        // Editör ortamında durdurma işlemi
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
