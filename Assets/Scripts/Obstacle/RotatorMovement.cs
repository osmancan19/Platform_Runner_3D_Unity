using UnityEngine;

public class RotatorMovement : MonoBehaviour
{
    private float rotationSpeed; // Dönme hızı

    [SerializeField]private bool reverse; // Ters dönüş kontrolü
    void Start()
    {
        rotationSpeed = 50f;
    }

    void Update()
    {
        // Dönüş yönünü kontrol et
        float actualRotationSpeed = reverse ? -rotationSpeed : rotationSpeed;

        // Z ekseninde sürekli olarak döndürme işlemi
        transform.Rotate(0, actualRotationSpeed * Time.deltaTime, 0);
    }
}
