using UnityEngine;

public class ShiningObstacleMovement : MonoBehaviour
{
    private float rotationSpeed;   // Y ekseninde dönme hızı
    private float moveSpeed;         // X ekseninde hareket hızı
    private float moveRange;         // X ekseninde ileri geri hareket aralığı
    public bool startInReverse;  // Yatay hareketin ters yönde başlamasını sağlar
    private Vector3 startPosition;
    private bool movingRight;
    public ParticleSystem obstacleParticle; // Engelin üzerindeki partikül sistemi

    void Start()
    {
        rotationSpeed = 200f;
        moveSpeed = 5f;
        moveRange = 7f;
        startPosition = transform.position; // Başlangıç pozisyonunu kaydediyoruz

        // Eğer startInReverse true ise sola başlar
        movingRight = !startInReverse; 
        
        // Partikül efektini başlat
        if (obstacleParticle != null)
        {
            obstacleParticle.Play();
        }
    }

    void Update()
    {
        // Y ekseni etrafında dönme
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // X ekseninde ileri geri hareket
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // Sağ sınırı geçerse sola dön
            if (transform.position.x >= startPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;

            // Sol sınırı geçerse sağa dön
            if (transform.position.x <= startPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }
}
