using UnityEngine;
using System.Collections;

public class ShiningObstacleCollision : MonoBehaviour
{
    public ParticleSystem obstacleParticle; // Engelin üzerindeki partikül sistemi
    private Gradient originalColorGradient; // Orijinal renk gradyanı

    void Start()
    {
        if (obstacleParticle != null)
        {
            // Orijinal Color over Lifetime gradyanını sakla
            var colorOverLifetime = obstacleParticle.colorOverLifetime;
            originalColorGradient = colorOverLifetime.color.gradient;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Çarpışan nesnede ICharacter interface'inin olup olmadığını kontrol et
        ICharacter character = collision.gameObject.GetComponent<ICharacter>();

        if (character != null && obstacleParticle != null) // Eğer ICharacter implementasyonu varsa ve partikül sistemi mevcutsa
        {
            // Yeni bir Color over Lifetime gradyanı oluşturur
            var colorOverLifetime = obstacleParticle.colorOverLifetime;
            colorOverLifetime.enabled = true;

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.yellow, 1.0f) }, // Renkler
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) } // Şeffaflık
            );

            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);

            // 1 saniye sonra eski rengine dönmesi için Coroutine başlat
            StartCoroutine(ResetColorOverLifetimeAfterDelay(2f));
        }
    }

    private IEnumerator ResetColorOverLifetimeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Rengi eski Color over Lifetime gradyanına döndür
        var colorOverLifetime = obstacleParticle.colorOverLifetime;
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(originalColorGradient);
    }
}
