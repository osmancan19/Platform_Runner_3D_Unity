using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPaintingController : MonoBehaviour
{
    public TextMeshProUGUI percentageText;
    public Slider brushSizeSlider;
    public Button redButton;
    private Color redButtonColor;
    public Button yellowButton;
    private Color yellowButtonColor;
    public Button blueButton;
    private Color blueButtonColor;
    public PaintableObject paintableObject;
    private Color textStartColor = Color.green; // %0 için başlangıç rengi (yeşil)
    private Color textEndColor = Color.red;     // %100 için bitiş rengi (kırmızı)

    void Start()
    {
        yellowButtonColor = new Color(1.0f, 0.87f, 0.39f, 1);
        redButtonColor = new Color(0.87f, 0.33f, 0.39f);
        blueButtonColor = new Color(0.33f, 0.75f, 1.0f, 1);
        
        // Renk butonlarına tıklama olaylarını ekleyin
        redButton.onClick.AddListener(() => SetBrushColor(redButtonColor));
        yellowButton.onClick.AddListener(() => SetBrushColor(yellowButtonColor));
        blueButton.onClick.AddListener(() => SetBrushColor(blueButtonColor));
        
        brushSizeSlider.onValueChanged.AddListener(SetBrushSize);
        UpdatePercentage(0); // Başlangıçta %0
    }

    void Update()
    {
        float paintedPercentage = paintableObject.GetPaintedPercentage();
        UpdatePercentage(paintedPercentage);
        
    }

    public void SetBrushColor(Color color)
    {
        paintableObject.SetBrushColor(color); // Brush rengini PaintableObject’e aktar
    }

    public void SetBrushSize(float size)
    {
        paintableObject.SetBrushSize(size); // Brush boyutunu PaintableObject’e aktar
    }

    public void UpdatePercentage(float percentage)
    {
        percentageText.text = "%" + Mathf.RoundToInt(percentage).ToString();
        
        // Yeşilden kırmızıya geçişi sağla
        percentageText.color = Color.Lerp(textStartColor, textEndColor, percentage / 100f);
    }
}
