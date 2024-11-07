using UnityEngine;

public class RotatingPlatformMovement : MonoBehaviour
{
    private float rotationSpeed;  // Platformun Z ekseninde dönme hızı
    [SerializeField]private bool rotateInReverse = false; // Platformun ters yönde dönmesini sağlar
    private int RotationDirection; // Dönüş yönünü belirten özellik

    void Start()
    {
        rotationSpeed = 50f;
        // Dönüş yönünü ayarla (ters ise -1, değilse 1)
        RotationDirection = rotateInReverse ? -1 : 1;
    }

    void Update()
    {
        // Platformun Z ekseninde sürekli dönmesini sağlıyoruz
        transform.Rotate(0, 0, rotationSpeed * RotationDirection * Time.deltaTime);
    }

    public int GetRotationDirection()
    {
        return RotationDirection;
    }
}
