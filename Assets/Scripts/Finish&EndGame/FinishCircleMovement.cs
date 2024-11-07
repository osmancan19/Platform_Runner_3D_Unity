using UnityEngine;

public class FinishCircleMovement : MonoBehaviour
{
    private float rotationSpeed; // Dönme hızı

    void Start ()
    {
        rotationSpeed = 20f;
    }
    void Update()
    {
        // Z ekseninde sürekli olarak döndürme işlemi
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
