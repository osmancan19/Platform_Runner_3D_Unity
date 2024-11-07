using UnityEngine;
using System.Collections;

public class MarkerMovement : MonoBehaviour
{
    private float moveRange; // Hareket edeceği mesafe (yukarı-aşağı)
    private float moveSpeed;   // Hareket hızı

    private Vector3 startPosition;
    private bool movingUp = true;
    private Quaternion initialRotation;

    void Awake()
    {
        // Başlangıç rotasyonunu kaydet
        initialRotation = transform.rotation;
    }

    void Start()
    {
        moveRange = 0.5f;
        moveSpeed = 1f;
        // Başlangıç pozisyonunu kaydet
        startPosition = transform.position;
    }

    void Update()
    {
        MoveUpDown();
        FaceForward();
    }

    private void MoveUpDown()
    {
        // Hareket yönünü belirle
        if (movingUp)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // Maksimum yukarı pozisyona ulaştığında yön değiştir
            if (transform.position.y >= startPosition.y + moveRange)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position -= Vector3.up * moveSpeed * Time.deltaTime;

            // Minimum aşağı pozisyona ulaştığında yön değiştir
            if (transform.position.y <= startPosition.y - moveRange)
            {
                movingUp = true;
            }
        }
    }

    private void FaceForward()
    {
        // Objenin her zaman başlangıç rotasyonuna (initialRotation) sabit kalmasını sağlar
        transform.rotation = initialRotation;
    }
}
