using UnityEngine;
using System.Collections;

public class PlayerMovement : CharacterMovement
{
    public override void Start()
    {
        base.Start();
        characterType = CharacterType.Player; 
    }

    void FixedUpdate()
    {
        if (isFell || !canMove) return;  // Düşme animasyonu aktifse hareket etmeye izin verme

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movement = movementInput.normalized * speed * Time.deltaTime; 

        if (movementInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            SetRunningAnimation(true);
        }
        else
        {
            SetRunningAnimation(false);
        }

        rb.MovePosition(transform.position + movement);
        CheckFalling();
    }

    public override void CheckFalling()
    {
        if (transform.position.y < fallThreshold && !isFell)
        {
            isFell = true;
            SetFallingAnimation(isFell);
            SetRunningAnimation(false);
            SoundManager.instance.PlayFailSound(); 
        }
    }

    // standing up animasyonunun sonunda çağrılacak metot
    public override void OnStandUpComplete()
    {
        isFell = false;
        SetFallingAnimation(isFell);
    }

    public override void ResetPosition()
    {
        transform.position = new Vector3(0, 1.537001f, 1f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
