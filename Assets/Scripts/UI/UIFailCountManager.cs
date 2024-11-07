using UnityEngine;
using TMPro;

public class UIFailCountManager : MonoBehaviour
{
    public static UIFailCountManager instance;
    private int failCount;
    [SerializeField] private TextMeshProUGUI failCounterText;

    void Awake() 
    {
        instance = this; 
        failCount = 0;  
    }

    public void IncreaseFailCount()
    {
        failCount++;
        failCounterText.text = ": " + failCount.ToString();
    }
}
