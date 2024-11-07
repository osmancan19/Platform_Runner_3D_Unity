using UnityEngine;

public class HalfDonutMovement : MonoBehaviour
{
    public Transform movingStick; // Hareket edecek olan child objeyi bu alana atıyoruz.
    private float minX;   // X eksenindeki minimum pozisyon
    private float maxX;    // X eksenindeki maksimum pozisyon
    public float speed;      // Hareket hızı
    private bool movingRight;

    void Start()
    {
        minX = -0.21f;
        maxX = 0.1410f;
        speed = 0.07f;
        movingRight = true;
    }
    void Update()
    {
        // Hareket yönünü belirle
        if (movingRight)
        {
            // X ekseninde ileri doğru hareket et
            movingStick.localPosition = new Vector3(
                Mathf.MoveTowards(movingStick.localPosition.x, maxX, speed * Time.deltaTime),
                movingStick.localPosition.y,
                movingStick.localPosition.z
            );

            // Maksimum pozisyona ulaştığında yön değiştir
            if (movingStick.localPosition.x >= maxX)
            {
                movingRight = false;
            }
        }
        else
        {
            // X ekseninde geri doğru hareket et
            movingStick.localPosition = new Vector3(
                Mathf.MoveTowards(movingStick.localPosition.x, minX, speed * Time.deltaTime),
                movingStick.localPosition.y,
                movingStick.localPosition.z
            );

            // Minimum pozisyona ulaştığında yön değiştir
            if (movingStick.localPosition.x <= minX)
            {
                movingRight = true;
            }
        }
    }
}