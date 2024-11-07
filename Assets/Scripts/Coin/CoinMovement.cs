using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    private float rotationSpeed; // Y ekseninde dönüş hızı
    private float floatSpeed; // Yukarı-aşağı hareket hızı
    private float floatHeight; // Yukarı-aşağı hareket yüksekliği

    private Vector3 startPosition;

    private void Start()
    {
        rotationSpeed = 150f;
        floatSpeed = 0.7f;
        floatHeight = 0.5f;
        startPosition = transform.position; // Başlangıç pozisyonunu kaydet
    }

    private void Update()
    {
        // Y ekseninde döndürme işlemi
        transform.Rotate(0, 0 , rotationSpeed * Time.deltaTime);

        // Yukarı-aşağı hareket
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
