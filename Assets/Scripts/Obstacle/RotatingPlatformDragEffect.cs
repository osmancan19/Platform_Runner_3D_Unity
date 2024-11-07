using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RotatingPlatformDragEffect : MonoBehaviour
{
    private float dragForce; // Karakterler üzerinde uygulanacak sürüklenme kuvveti
    private RotatingPlatformMovement rotatingPlatformMovement;

    private void Start()
    {
        dragForce = 6f;
        rotatingPlatformMovement = GetComponent<RotatingPlatformMovement>();
    }

    private void OnCollisionStay(Collision other)
    {
        Rigidbody characterRb = other.rigidbody;
        Vector3 forceDirection;
        int rotationDirection = rotatingPlatformMovement.GetRotationDirection();

        if(rotationDirection >0)
        {
            if(other.transform.position.x <= -0.8f)
            {
                forceDirection = new Vector3(Mathf.Sign(rotationDirection*-1)*2, 0, 0);     
            }
            else
            {
                forceDirection = new Vector3(Mathf.Sign(rotationDirection*-1), 1.6f, 0);    
            }
        }
        else
        {
            if(other.transform.position.x >= 0.8f)
            {
                forceDirection = new Vector3(Mathf.Sign(rotationDirection*-1)*2, 0, 0);     
            }
            else
            {
                forceDirection = new Vector3(Mathf.Sign(rotationDirection*-1), 1.6f, 0);    
            }
        }
        characterRb.AddForce(forceDirection * dragForce, ForceMode.Force);
    }
}
