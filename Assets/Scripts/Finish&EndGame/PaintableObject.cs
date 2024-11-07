using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PaintableObject: MonoBehaviour
{
    RenderTexture maskTexture;
    Camera maskCamera;
    public Shader maskShader;
    private float brushSize; // Varsayılan brush size
    private Color brushColor; // Varsayılan brush color
    private bool isPaintable;
    void Start() 
    {
        isPaintable = false;
        brushSize = 0.2f;
        brushColor = new Color(0.87f,0.33f,0.39f,1);
        maskTexture = new RenderTexture(512,512,0,RenderTextureFormat.ARGB32);

        // maskTexture'ı beyaz olarak başlat
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = maskTexture;
        GL.Clear(true, true, Color.white); // Tüm texture'ı beyaz renkle doldur
        RenderTexture.active = activeRT;

        GameObject mGameObject = new GameObject("CanvasCamera");
        mGameObject.transform.position = new Vector3(0,0,215);
        maskCamera = mGameObject.AddComponent<Camera>();
        maskCamera.targetTexture = maskTexture;
        maskCamera.enabled = false;
        maskCamera.cullingMask = (1 << LayerMask.NameToLayer("PaintableLayer"));
        maskCamera.clearFlags = CameraClearFlags.Nothing;
        maskCamera.orthographic = true;
        maskCamera.orthographicSize = 10f;
        maskCamera.farClipPlane = 75f;
        maskCamera.SetReplacementShader(maskShader,"");
        GetComponent<Renderer>().material.SetTexture("_MainTex", maskTexture);
        GetComponent<Renderer>().material.SetTexture("_MaskTex", maskTexture);
    }

    public void PaintMask(Vector3 pos, Vector3 normal)
    {
        if(isPaintable)
        {
            GetComponent<Renderer>().enabled = true;
            maskCamera.transform.position = pos + normal;
            maskCamera.transform.rotation = Quaternion.LookRotation(-normal);
            Material material = GetComponent<Renderer>().material;
            material.SetFloat("_HitPosX", pos.x);
            material.SetFloat("_HitPosY", pos.y);
            material.SetFloat("_HitPosZ", pos.z);
            material.SetFloat("_BrushSize", brushSize); // brush size shader'a aktarılıyor
            material.SetColor("_BrushColor", brushColor); // brush color shader'a aktarılıyor
            maskCamera.Render();
            GetComponent<Renderer>().enabled = false;
        }
    }
    public void SetBrushSize(float size)
    {
        brushSize = size;
    }

    public void SetBrushColor(Color color)
    {
        brushColor = color;
    }

    public float GetPaintedPercentage()
    {
        // RenderTexture'ı Texture2D'ye kopyala
        RenderTexture.active = maskTexture;
        Texture2D texture = new Texture2D(maskTexture.width, maskTexture.height, TextureFormat.ARGB32, false);
        texture.ReadPixels(new Rect(0, 0, maskTexture.width, maskTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;

        // Boyalı pikselleri say
        int paintedPixels = 0;
        int totalPixels = texture.width * texture.height;
        Color32[] pixels = texture.GetPixels32();

        foreach (var pixel in pixels)
        {
            // Sadece tamamen beyaz olmayan pikselleri boyalı olarak sayıyoruz
            if (pixel.r < 255 || pixel.g < 255 || pixel.b < 255) // Beyazdan farklı renkler
            {
                paintedPixels++;
            }
        }

        // Texture2D nesnesini yok et
        Destroy(texture);

        if((float)paintedPixels / totalPixels * 100.0f == 100)
        {
            Camera.main.GetComponent<PostProcessVolume>().isGlobal = true;
            UIManager.Instance.ShowGameOverUI();
        }
        // Yüzdeyi hesapla
        return (float)paintedPixels / totalPixels * 100.0f;
    }

    public void IsPaintable(bool isPaintable)
    {
        this.isPaintable = isPaintable;
    }

}
